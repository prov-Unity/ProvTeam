using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [ReadOnly, SerializeField] private float speedMove;
    [ReadOnly, SerializeField] private float speedJump;

    // private bool canMove;
    // [HideInInspector] public bool canJump;
    // [HideInInspector] public bool isGrounded;

    // private Player player;
    // private Rigidbody curRigidbody;

    // private void Awake() {
    //     speedMove = 5f;
    //     speedJump = 7f;

    //     canMove = false;
    //     canJump = false;
    //     isGrounded = false;

    //     player = GetComponent<Player>();
    //     curRigidbody = GetComponent<Rigidbody>();
    // }

    // private void Update() {
    //     UpdateIsGrounded();
    // }

    // public void Move(float inputX, float inputY) {
    //     curRigidbody.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + inputX, transform.position.y, transform.position.z + inputY), speedMove * Time.deltaTime);

    //     player.playerAnimation.UpdateMoveInfo(inputX, inputY);
    // }

    // public void Roll() {
    //     player.playerAnimation.PlayRollAnimation();
    // }

    // private void UpdateIsGrounded() {
    //     RaycastHit hitInfo;
    //     Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity);
    //     if(hitInfo.distance >= 1f)
    //         isGrounded = false;
    //     else
    //         isGrounded = true;  

    //     player.playerAnimation.UpdateGroundInfo(hitInfo.distance, isGrounded);
    // }

    // public void Jump() {
    //     canJump = isGrounded && !player.playerCombat.isAttacking; 
    //     player.playerAnimation.UpdateCanJump();
    //     if(canJump) {
    //         player.playerAnimation.PlayJumpAnimation();
    //         curRigidbody.AddForce(Vector3.up * speedJump, ForceMode.Impulse);
    //     }
    // }
}