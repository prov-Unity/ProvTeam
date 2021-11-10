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
    [HideInInspector] public PlayerCombat playerCombat;

    private void Awake() {
        neckTransform = FindObjectOfType<PlayerNeck>().transform;

        playerInfo = GetComponent<PlayerInfo>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerCombat = GetComponent<PlayerCombat>();
    }
}