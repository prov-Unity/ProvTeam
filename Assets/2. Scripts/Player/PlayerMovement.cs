using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    public Rigidbody curRigidbody;

    private void Awake() {
        player = GetComponent<Player>();
        curRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        UpdateIsGrounded();
    }

    public void Move(float inputX, float inputY) {
        if(!player.playerInfo.isAttacking) {
            if(player.playerInfo.isRolling)
                curRigidbody.position += transform.forward * player.playerInfo.speedRoll * Time.deltaTime;
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

    public void Roll(float inputX, float inputY) {
        if(!player.playerInfo.isRolling && player.playerInfo.isGrounded && !player.playerInfo.isAttacking) {
            player.playerAnimation.PlayRollAnimation();
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
        player.playerInfo.canJump = !player.playerInfo.isRolling && player.playerInfo.isGrounded && !player.playerInfo.isAttacking; 
        player.playerAnimation.UpdateCanJump();
        if(player.playerInfo.canJump) {
            player.playerAnimation.PlayJumpAnimation();
            curRigidbody.AddForce(Vector3.up * player.playerInfo.speedJump, ForceMode.Impulse);
        }
    }
}