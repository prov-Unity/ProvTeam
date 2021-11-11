using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Transform neckTransform;

    [HideInInspector] public PlayerInfo playerInfo;
    [HideInInspector] public PlayerInputManager playerInputManager;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public PlayerWeaponSlot playerLeftWeaponSlot;
    [HideInInspector] public PlayerWeaponSlot playerRightWeaponSlot;
    [HideInInspector] public PlayerCombat playerCombat;

    private void Awake() {
        neckTransform = FindObjectOfType<PlayerNeck>().transform;

        playerInfo = GetComponent<PlayerInfo>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        PlayerWeaponSlot[] slots = GetComponentsInChildren<PlayerWeaponSlot>();
        playerLeftWeaponSlot = slots[0];
        playerRightWeaponSlot = slots[1];
        playerCombat = GetComponent<PlayerCombat>();
    }
}