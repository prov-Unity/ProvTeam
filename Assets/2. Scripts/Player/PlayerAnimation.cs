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
        if(curWeaponTypeIndex < (int)WeaponType.Axe_01)
            animator.SetInteger("CurWeaponIndex", 0);
        else if(curWeaponTypeIndex < (int)WeaponType.Spear_01)
            animator.SetInteger("CurWeaponIndex", 1);
        else if(curWeaponTypeIndex < (int)WeaponType.TwoHandSword_01)
            animator.SetInteger("CurWeaponIndex", 2);
        else
            animator.SetInteger("CurWeaponIndex", 3);
    }

    public void UpdateGroundInfo(float groundDistance, bool isGrounded) {
        animator.SetFloat("GroundDistance", groundDistance);
        animator.SetBool("IsGrounded", isGrounded); 
    }

    public void PlayAttackAnimation() {
        animator.SetInteger("AttackIndex", player.playerInfo.attackIndex);  
        switch(player.playerInfo.curWeapon.weaponType) {
            case WeaponType.Fist_Left: animator.SetTrigger("AttackFist"); break;
            case WeaponType.Bone:
            case WeaponType.Sword_02: animator.SetTrigger("AttackSword"); break;
            case WeaponType.Dagger_01:
            case WeaponType.Dagger_02: animator.SetTrigger("AttackDagger"); break;
            case WeaponType.Mace_01:
            case WeaponType.Mace_02: animator.SetTrigger("AttackMace"); break;
            case WeaponType.Axe_01:
            case WeaponType.Axe_02:
            case WeaponType.Axe_03: animator.SetTrigger("AttackAxe"); break;
            case WeaponType.Spear_01:
            case WeaponType.Spear_02: animator.SetTrigger("AttackSpear"); break;
            case WeaponType.TwoHandSword_01:
            case WeaponType.TwoHandSword_02: animator.SetTrigger("Attack2Hand"); break;
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