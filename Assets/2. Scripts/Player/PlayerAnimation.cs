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
        switch(player.playerInfo.weaponType) {
            case WeaponType.Fist: animator.SetTrigger("AttackFist"); break;
            case WeaponType.Bone: break;
            case WeaponType.Ornate_Sword: animator.SetTrigger("Attack2Hand"); break;
        }   
    }

    private IEnumerator CheckComboLimit() {
        yield return new WaitForSeconds(player.playerInfo.comboLimitTime);
        player.playerInfo.attackIndex = 0;
    }

    public void PlayHitAnimation() {
        if(player.playerInfo.health > 0) {
            switch(player.playerInfo.weaponType) {
                case WeaponType.Fist: animator.SetTrigger("HitFist"); break;
                case WeaponType.Bone: break;
                case WeaponType.Ornate_Sword: animator.SetTrigger("Hit2Hand"); break;
            }
        }
        else {
            switch(player.playerInfo.weaponType) {
                case WeaponType.Fist: animator.SetTrigger("DeathFist"); break;
                case WeaponType.Bone: break;
                case WeaponType.Ornate_Sword: animator.SetTrigger("Death2Hand"); break;
            }
        }
    }

    public void PlayJumpAnimation() {
        switch(player.playerInfo.weaponType) {
            case WeaponType.Fist: animator.SetTrigger("JumpFist"); break;
            case WeaponType.Bone: break;
            case WeaponType.Ornate_Sword: animator.SetTrigger("Jump2Hand"); break;
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
