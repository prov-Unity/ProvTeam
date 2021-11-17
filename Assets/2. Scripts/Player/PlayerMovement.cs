using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private Rigidbody curRigidbody;

    private void Awake() {
        player = GetComponent<Player>();
        curRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        UpdateIsGrounded();
    }

    public void Move(float inputX, float inputY) {
        curRigidbody.position += transform.forward * inputY * player.playerInfo.speedMove * Time.deltaTime;
        curRigidbody.position += transform.right * inputX * player.playerInfo.speedMove * Time.deltaTime;
        if(inputY == 1)
            RotateBasedOnCamera();

        player.playerAnimation.UpdateMoveInfo(inputX, inputY);
    }

    public void RotateBasedOnCamera() {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }

    public void Roll() {
        player.playerAnimation.PlayRollAnimation();
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
        player.playerInfo.canJump = player.playerInfo.isGrounded && !player.playerInfo.isAttacking; 
        player.playerAnimation.UpdateCanJump();
        if(player.playerInfo.canJump) {
            player.playerAnimation.PlayJumpAnimation();
            curRigidbody.AddForce(Vector3.up * player.playerInfo.speedJump, ForceMode.Impulse);
        }
    }
}