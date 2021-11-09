using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private float inputX;
    private float inputY;


    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        // inputX = Input.GetAxisRaw("Horizontal");
        // inputY = Input.GetAxisRaw("Vertical");
        // player.playerMovement.Move(inputX, inputY);

        // if(Input.GetKeyDown(KeyCode.Space))
        //     player.playerMovement.Jump();

        // if(Input.GetKeyDown(KeyCode.LeftShift))
        //     player.playerMovement.Roll();

        // if(!player.playerCombat.isAttacking && Input.GetMouseButtonDown(0))
        //     player.playerCombat.Attack();
    }
}