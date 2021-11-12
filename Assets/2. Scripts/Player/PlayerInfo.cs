using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [ReadOnly] public float health;
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
        health = 100f;
        speedMove = 5f;
        speedJump = 7f;

        weaponType = WeaponType.Fist_Left;
        attackIndex = -1;
        comboLimitTime = 2f;

        canJump = false;
        isGrounded = false;
        isAttacking = false;

        player = GetComponentInChildren<Player>();
    }
}
