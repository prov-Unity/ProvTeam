using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [ReadOnly] public int maxHealth;
    [ReadOnly] public int health;

    [ReadOnly] public float speedMove;
    [ReadOnly] public float speedJump;

    [ReadOnly] public int attackIndex;
    [ReadOnly] public float comboLimitTime;

    [ReadOnly] public bool canJump;
    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isAttacking;
    [ReadOnly] public bool isGettingHit;
    [ReadOnly] public bool isRolling;
    [ReadOnly] public bool isInvulnerable;

    private Player player;

    [ReadOnly] public AvailableWeapon curWeapon;
    [ReadOnly] public List<AvailableWeapon> availableWeapons;


    private void Awake() {
        maxHealth = 100;
        health = maxHealth;
        
        speedMove = 2f;
        speedJump = 5f;

        attackIndex = 0;
        comboLimitTime = 2f;

        canJump = false;
        isGrounded = false;
        isAttacking = false;
        isGettingHit = false;
        isRolling = false;
        isInvulnerable = false;

        player = GetComponentInChildren<Player>();     

        curWeapon = new AvailableWeapon(WeaponType.Fist_Left, -1);
    }
    
    private void Start() {        
        UIManager.instance.EnableWeaponSelectionPopup();
        WeaponSelectionManager.instance.UpdateWeaponSelectionBox();
        UIManager.instance.UpdateCurWeaponInfo();
        UIManager.instance.DisableWeaponSelectionPopup();
    }
}