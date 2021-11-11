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
        curRigidbody.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + inputX, transform.position.y, transform.position.z + inputY), player.playerInfo.speedMove * Time.deltaTime);

        player.playerAnimation.UpdateMoveInfo(inputX, inputY);
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