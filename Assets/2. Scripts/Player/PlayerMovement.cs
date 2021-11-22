using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [ReadOnly] public Rigidbody curRigidbody;

    private Player player;
    
    private float invulnerableResetTime;

    private void Awake() {
        player = GetComponent<Player>();
        curRigidbody = GetComponent<Rigidbody>();

        invulnerableResetTime = 0.5f;
    }

    private void Update() {
        UpdateIsGrounded();
    }

    public void Move(float inputX, float inputY) {
        if(!player.playerInfo.isAttacking && !player.playerInfo.isGettingHit) {
            if(player.playerInfo.isRolling)
                curRigidbody.position += transform.forward * Time.deltaTime;
            else {
                curRigidbody.position += transform.forward * inputY * player.playerInfo.speedMove * Time.deltaTime;
                curRigidbody.position += transform.right * inputX * player.playerInfo.speedMove * Time.deltaTime;

                if(inputX != 0 || inputY != 0)
                    RotateBasedOnCamera();
            }
            player.playerAnimation.UpdateMoveInfo(inputX, inputY);
        }
    }

    private void RotateBasedOnCamera() {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
    
    private void RotateBasedOnKey(float inputX, float inputY) {
        transform.forward = transform.forward * inputY + transform.right * inputX; 
    }

    private IEnumerator ResetIsInvulnerableAfterSomeTime() {
        yield return new WaitForSeconds(invulnerableResetTime);
        player.playerInfo.isInvulnerable = false;
    }

    public void Roll(float inputX, float inputY) {
        if(!player.playerInfo.isRolling && player.playerInfo.isGrounded && !player.playerInfo.isAttacking && !player.playerInfo.isGettingHit) {
            player.playerAnimation.PlayRollAnimation();
            player.playerInfo.isInvulnerable = true;
            StartCoroutine("ResetIsInvulnerableAfterSomeTime");
            player.playerInfo.isRolling = true;

            RotateBasedOnKey(inputX, inputY);
        }
    }
    public void SetIsRollingFalse() {
        player.playerInfo.isRolling = false;
        RotateBasedOnCamera();
    }

    private void UpdateIsGrounded() {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity);
        if(hitInfo.distance >= 0.3f)
            player.playerInfo.isGrounded = false;
        else
            player.playerInfo.isGrounded = true;  

        player.playerAnimation.UpdateGroundInfo(hitInfo.distance, player.playerInfo.isGrounded);
    }

    public void Jump() {
        player.playerInfo.canJump = !player.playerInfo.isRolling && player.playerInfo.isGrounded && !player.playerInfo.isAttacking && !player.playerInfo.isGettingHit; 
        player.playerAnimation.UpdateCanJump();
        if(player.playerInfo.canJump) {
            player.playerAnimation.PlayJumpAnimation();
            curRigidbody.AddForce(Vector3.up * player.playerInfo.speedJump, ForceMode.Impulse);
        }
    }
}