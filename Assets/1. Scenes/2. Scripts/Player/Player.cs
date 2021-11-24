using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Transform cameraTargetTransform;

    [HideInInspector] public PlayerInfo playerInfo;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAnimation playerAnimation;

    [HideInInspector] public PlayerWeaponSlot playerLeftWeaponSlot;
    [HideInInspector] public PlayerWeaponSlot playerRightWeaponSlot;
    [HideInInspector] public List<PlayerWeaponSpawnPoint> playerWeaponSpawnPoints;
    [HideInInspector] public PlayerCombat playerCombat;
    [HideInInspector] public PlayerInteraction playerInteraction;
    [HideInInspector] public PlayerDetector playerDetector;


    private void Awake() {
        cameraTargetTransform = FindObjectOfType<PlayerCameraTarget>().transform;

        playerInfo = GetComponent<PlayerInfo>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        PlayerWeaponSlot[] slots = GetComponentsInChildren<PlayerWeaponSlot>();
        playerLeftWeaponSlot = slots[0];
        playerRightWeaponSlot = slots[1];
        playerWeaponSpawnPoints = new List<PlayerWeaponSpawnPoint>();
        foreach(PlayerWeaponSpawnPoint curPoint in playerLeftWeaponSlot.GetComponentsInChildren<PlayerWeaponSpawnPoint>()) {
            playerWeaponSpawnPoints.Add(curPoint);
        }
        foreach(PlayerWeaponSpawnPoint curPoint in playerRightWeaponSlot.GetComponentsInChildren<PlayerWeaponSpawnPoint>()) {
            playerWeaponSpawnPoints.Add(curPoint);
        }

        playerCombat = GetComponent<PlayerCombat>();
        playerInteraction = GetComponent<PlayerInteraction>();
        playerDetector = GetComponent<PlayerDetector>();
    }
}