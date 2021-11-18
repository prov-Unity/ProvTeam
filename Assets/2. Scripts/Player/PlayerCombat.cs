using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [ReadOnly, SerializeField] private Weapon leftWeapon;
    [ReadOnly, SerializeField] private Weapon rightWeapon;
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
            case "Weapon": GetDamaged(other.GetComponent<Weapon>().attackPower); break;
            case "Death": StartCoroutine("Die"); break;
        }
    }

    private void GetDamaged(int attackPower) {
        player.playerInfo.health -= attackPower;

        if(player.playerInfo.health > 0)
            player.playerAnimation.PlayHitAnimation();
        else {
            player.playerAnimation.PlayDeathAnimation();
            StartCoroutine("Die");
        }

        UIManager.instance.UpdatePlayerHealthBar();
    }

    private IEnumerator Die() {
        curCollider.enabled = false;
        player.playerMovement.curRigidbody.useGravity = false;
        yield return new WaitForSeconds(3f);
        // do something 
    }
}