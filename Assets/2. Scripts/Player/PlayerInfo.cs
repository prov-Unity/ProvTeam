using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [ReadOnly] public float health;
    [ReadOnly] public float speedMove;
    [ReadOnly] public float speedJump;
    [ReadOnly] public int attackIndex;
    [ReadOnly] public float comboLimitTime;

    [ReadOnly] public bool canJump;
    [ReadOnly] public bool isGrounded;
    [ReadOnly] public bool isAttacking;

    private Player player;
    private void Awake() {
        health = 100f;
        speedMove = 5f;
        speedJump = 7f;
        attackIndex = -1;
        comboLimitTime = 2f;

        canJump = false;
        isGrounded = false;
        isAttacking = false;

        player = GetComponentInChildren<Player>();
    }
}
