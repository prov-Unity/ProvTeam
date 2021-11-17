using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [ReadOnly] public int maxHealth;
    [ReadOnly] public int health;

    [ReadOnly] public float speedMove;
    [ReadOnly] public float speedJump;
    [ReadOnly] public float speedRoll;

    [ReadOnly] public int attackIndex;
    [ReadOnly] public float comboLimitTime;

    [ReadOnly] public bool canJump;
    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isAttacking;
    [ReadOnly] public bool isRolling;

    private Player player;

    [ReadOnly] public AvailableWeapon curWeapon;
    [ReadOnly] public List<AvailableWeapon> availableWeapons;


    private void Awake() {
        maxHealth = 100;
        health = maxHealth;
        
        speedMove = 3f;
        speedJump = 5f;
        speedRoll = 6.5f;

        attackIndex = 0;
        comboLimitTime = 2f;

        canJump = false;
        isGrounded = false;
        isAttacking = false;
        isRolling = false;

        player = GetComponentInChildren<Player>();
    }
    
        private void Start() {
        // these codes would be altered after save/load functionality is implemented
        availableWeapons = new List<AvailableWeapon>();
        availableWeapons.Add(new AvailableWeapon(WeaponType.Fist_Left, WeaponManager.instance.weaponInitialDurabilities[(int)WeaponType.Fist_Left]));
        availableWeapons.Add(new AvailableWeapon(WeaponType.Bone_Right, WeaponManager.instance.weaponInitialDurabilities[(int)WeaponType.Bone_Right] - 1));
        curWeapon = availableWeapons[0];
    }
}