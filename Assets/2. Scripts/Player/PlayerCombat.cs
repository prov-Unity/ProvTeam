using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [ReadOnly, SerializeField] private Weapon curWeapon;
    private Player player;

    private void Awake() {
        player = GetComponentInChildren<Player>();
        curWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update() {
        ;
    }

    public void Attack() {
        player.playerInfo.isAttacking = true;     
        
        player.playerAnimation.PlayAttackAnimation();
    }

    public void EnableWeaponCollider() {
        curWeapon.EnableCollider();
    }

    public void DisableWeaponCollider() {
        curWeapon.DisableCollider();
    }

    public void SetIsAttackingFalse() {
        player.playerInfo.isAttacking = false;
    }

    public void GetDamaged(float attackPower) {
        player.playerInfo.health -= attackPower;

        player.playerAnimation.PlayHitAnimation();
    }
}