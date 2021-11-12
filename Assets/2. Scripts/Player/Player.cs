using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Transform neckTransform;
    [HideInInspector] public PlayerWeaponSpawnPoint[] playerWeaponSpawnPoints;

    [HideInInspector] public PlayerInfo playerInfo;
    [HideInInspector] public PlayerInputManager playerInputManager;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public PlayerWeaponSlot playerLeftWeaponSlot;
    [HideInInspector] public PlayerWeaponSlot playerRightWeaponSlot;
    [HideInInspector] public PlayerCombat playerCombat;

    private void Awake() {
        neckTransform = FindObjectOfType<PlayerNeck>().transform;
        playerWeaponSpawnPoints = FindObjectsOfType<PlayerWeaponSpawnPoint>();

        playerInfo = GetComponent<PlayerInfo>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        PlayerWeaponSlot[] slots = GetComponentsInChildren<PlayerWeaponSlot>();
        playerLeftWeaponSlot = slots[0];
        playerRightWeaponSlot = slots[1];
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void Start() {
        // these codes would be changed after save/load functionality is implemented
        playerLeftWeaponSlot.SelectWeapon(WeaponType.Fist_Left);
        playerRightWeaponSlot.SelectWeapon(WeaponType.Fist_Right);

        playerCombat.SetWeapons(playerLeftWeaponSlot.curWeapon.GetComponent<Weapon>(), playerLeftWeaponSlot.curWeapon.GetComponent<Weapon>());
    }
}