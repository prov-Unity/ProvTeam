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

    public void UpdateGroundInfo(float groundDistance, bool isGrounded) {
        animator.SetFloat("GroundDistance", groundDistance);
        animator.SetBool("IsGrounded", isGrounded); 
    }

    public void PlayAttackAnimation() {
        animator.SetInteger("AttackIndex", player.playerInfo.attackIndex);  
        switch(player.playerInfo.curWeapon) {
            case WeaponType.Fist_Left: animator.SetTrigger("AttackFist"); break;
            case WeaponType.Bone_Right: animator.SetTrigger("Attack2Hand"); break;
        }   
    }

    public void PlayHitAnimation() {
        if(player.playerInfo.health > 0) {
            switch(player.playerInfo.curWeapon) {
                case WeaponType.Fist_Left: animator.SetTrigger("HitFist"); break;
                case WeaponType.Bone_Right: animator.SetTrigger("Hit2Hand"); break;
            }
        }
        else {
            switch(player.playerInfo.curWeapon) {
                case WeaponType.Fist_Left: animator.SetTrigger("DeathFist"); break;
                case WeaponType.Bone_Right: animator.SetTrigger("Death2Hand"); break;
            }
        }
    }

    public void PlayJumpAnimation() {
        switch(player.playerInfo.curWeapon) {
            case WeaponType.Fist_Left: animator.SetTrigger("JumpFist"); break;
            case WeaponType.Bone_Right: animator.SetTrigger("Jump2Hand"); break;
        }
    }
    
    public void PlayRollAnimation() {
        animator.SetTrigger("Roll");
    }

    public void ChangeMoveTo2Hand() {
        animator.SetTrigger("ChangeWeaponTo2Hand");
    }

    public void ChangeMoveToFist() {
        animator.SetTrigger("ChangeWeaponToFist");
    }
}
