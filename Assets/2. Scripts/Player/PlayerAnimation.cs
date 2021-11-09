using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator animator; 
    private Coroutine comboCoroutineInfo;

    private void Awake() {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    public void UpdateCanJump() {
        animator.SetBool("CanJump", player.playerInfo.canJump);
    }

    public void UpdateMoveInfo(float inputX, float inputY) {
        animator.SetFloat("VelocityX", inputX);
        animator.SetFloat("VelocityY", inputY);
    }

    public void UpdateGroundInfo(float groundDistance, bool isGrounded) {
        animator.SetFloat("GroundDistance", groundDistance);
        animator.SetBool("IsGrounded", isGrounded); 
    }

    public void PlayAttackAnimation() {
        if(comboCoroutineInfo != null) 
            StopCoroutine("CheckComboLimit");
        comboCoroutineInfo = StartCoroutine("CheckComboLimit");

        player.playerInfo.attackIndex++;
        if(player.playerInfo.attackIndex > 5) 
            player.playerInfo.attackIndex = 0;    
        animator.SetInteger("AttackIndex", player.playerInfo.attackIndex);  
        animator.SetTrigger("Attack");     
    }

    private IEnumerator CheckComboLimit() {
        yield return new WaitForSeconds(player.playerInfo.comboLimitTime);
        player.playerInfo.attackIndex = 0;
    }

    public void PlayHitAnimation() {
        if(player.playerInfo.health > 0)
            animator.SetTrigger("Hit");
        else
            animator.SetTrigger("Death");
    }

    public void PlayJumpAnimation() {
        animator.SetTrigger("Jump");
    }
    
    public void PlayRollAnimation() {
        animator.SetTrigger("Roll");
    }
}
