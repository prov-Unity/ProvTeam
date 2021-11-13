using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [ReadOnly] public int maxHealth;
    [ReadOnly] public int health;
    [ReadOnly] public float speedMove;
    [ReadOnly] public float speedJump;

    [ReadOnly] public WeaponType weaponType;
    [ReadOnly] public int attackIndex;
    [ReadOnly] public float comboLimitTime;

    [ReadOnly] public bool canJump;
    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isAttacking;

    private Player player;
    private void Awake() {
        // these codes would be altered after save/load functionality is implemented
        maxHealth = 100;
        health = maxHealth;
        speedMove = 3f;
        speedJump = 5f;

        weaponType = WeaponType.Fist_Left;
        attackIndex = 0;
        comboLimitTime = 2f;

        canJump = false;
        isGrounded = false;
        isAttacking = false;

        player = GetComponentInChildren<Player>();
    }
}