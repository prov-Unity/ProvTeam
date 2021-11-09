using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [ReadOnly] public Transform neckTransform;
    [ReadOnly] public PlayerInput playerInput;
    [ReadOnly] public PlayerMovement playerMovement;
    [ReadOnly] public PlayerCombat playerCombat;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }
}