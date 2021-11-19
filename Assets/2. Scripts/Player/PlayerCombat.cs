using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [ReadOnly, SerializeField] private Weapon leftWeapon;
    [ReadOnly, SerializeField] private Weapon rightWeapon;
    [ReadOnly, SerializeField] private Weapon monsterWeapon;
    private Player player;
    private Collider curCollider;
    private Coroutine comboCoroutineInfo;

    private void Awake() {
        player = GetComponent<Player>();
        curCollider = GetComponent<Collider>();
    }

    public void SetWeapons(Weapon inputLeftWeapon, Weapon inputRightWeapon) {
        leftWeapon = inputLeftWeapon;
        rightWeapon = inputRightWeapon;
    }

    public void Attack() {
        if(!player.playerInfo.isRolling && player.playerInfo.isGrounded && !player.playerInfo.isAttacking) {
            player.playerInfo.isAttacking = true; 

            if(comboCoroutineInfo != null) 
                StopCoroutine("CheckComboLimit");
            comboCoroutineInfo = StartCoroutine("CheckComboLimit");

            player.playerAnimation.PlayAttackAnimation();

            player.playerInfo.attackIndex++;
            switch(player.playerInfo.curWeapon.weaponType) {
                case WeaponType.Fist_Left:
                    if(player.playerInfo.attackIndex > 3) 
                        player.playerInfo.attackIndex = 0;   
                break;
                case WeaponType.Bone_Right:
                    if(player.playerInfo.attackIndex > 5) 
                        player.playerInfo.attackIndex = 0;   
                break;
            }
        }
    }

    private IEnumerator CheckComboLimit() {
        yield return new WaitForSeconds(player.playerInfo.comboLimitTime);
        player.playerInfo.attackIndex = 0;
    }

    public void EnableLeftWeaponCollider() {
        leftWeapon.EnableCollider(true);
    }
    public void DisableLeftWeaponCollider() {
        leftWeapon.EnableCollider(false);
    }

    public void EnableRightWeaponCollider() {
        rightWeapon.EnableCollider(true);
    }
    public void DisableRightWeaponCollider() {
        rightWeapon.EnableCollider(false);
    }

    public void SetIsAttackingFalse() {
        player.playerInfo.isAttacking = false;
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "Weapon": 
            monsterWeapon = other.GetComponent<Weapon>();
            if(monsterWeapon.owner == "Monster")
                GetDamaged(monsterWeapon.attackPower); 
            break;
            case "DeadZone": StartCoroutine("Die"); break;
        }
    }

    private void GetDamaged(int attackPower) {
        player.playerInfo.health -= attackPower;

        if(player.playerInfo.health > 0) {
            player.playerAnimation.PlayHitAnimation();
            SetIsAttackingFalse();
            UIManager.instance.UpdatePlayerHealthBar();
        }
        else
            StartCoroutine("Die");
    }

    private IEnumerator Die() {
        player.playerInfo.health = 0;
        UIManager.instance.UpdatePlayerHealthBar();
        player.playerAnimation.PlayDeathAnimation();

        curCollider.enabled = false;
        player.playerMovement.curRigidbody.useGravity = false;
        yield return new WaitForSeconds(3f);
        // do something 
        // probably I will make a method of which name is might be Revive or Respawn from the Game Manager
        // that method would enable collider and gravity of player again, and do something
        // do something including reset attack index, reset is attacking, or so
    }
}