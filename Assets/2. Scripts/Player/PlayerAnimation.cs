using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator animator; 
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

    public void UpdateCurWeaponIndex() {
        int curWeaponTypeIndex = ((int)GameManager.instance.player.playerInfo.curWeapon.weaponType);
        if(curWeaponTypeIndex <= (int)WeaponType.Bone_Right)
            animator.SetInteger("CurWeaponIndex", 0);
        else
            animator.SetInteger("CurWeaponIndex", 1);
    }

    public void UpdateGroundInfo(float groundDistance, bool isGrounded) {
        animator.SetFloat("GroundDistance", groundDistance);
        animator.SetBool("IsGrounded", isGrounded); 
    }

    public void PlayAttackAnimation() {
        animator.SetInteger("AttackIndex", player.playerInfo.attackIndex);  
        switch(player.playerInfo.curWeapon.weaponType) {
            case WeaponType.Fist_Left: animator.SetTrigger("AttackFist"); break;
            case WeaponType.Bone_Right: animator.SetTrigger("Attack2Hand"); break;
        }   
    }

    public void PlayHitAnimation() {
        animator.SetTrigger("Hit"); 
    }

    public void PlayDeathAnimation() {
        animator.SetTrigger("Death");
    }

    public void PlayJumpAnimation() {
        animator.SetTrigger("Jump");
    }
    
    public void PlayRollAnimation() {
        animator.SetTrigger("Roll");
    }
}