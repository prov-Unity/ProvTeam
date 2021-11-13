using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [ReadOnly, SerializeField] private Weapon leftWeapon;
    [ReadOnly, SerializeField] private Weapon rightWeapon;
    private Player player;
    private Coroutine comboCoroutineInfo;

    private void Awake() {
        player = GetComponentInChildren<Player>();
    }

    private void Update() {
        ;
    }

    public void SetWeapons(Weapon inputLeftWeapon, Weapon inputRightWeapon) {
        leftWeapon = inputLeftWeapon;
        rightWeapon = inputRightWeapon;
    }

    public void Attack() {
        player.playerInfo.isAttacking = true; 

        if(comboCoroutineInfo != null) 
            StopCoroutine("CheckComboLimit");
        comboCoroutineInfo = StartCoroutine("CheckComboLimit");

        player.playerAnimation.PlayAttackAnimation();

        player.playerInfo.attackIndex++;
        switch(player.playerInfo.weaponType) {
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

    public void GetDamaged(int attackPower) {
        player.playerInfo.health -= attackPower;

        player.playerAnimation.PlayHitAnimation();
    }
}