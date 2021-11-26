using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [ReadOnly, SerializeField] private Weapon leftWeapon;
    [ReadOnly, SerializeField] private Weapon rightWeapon;
    [ReadOnly, SerializeField] private Weapon monsterWeapon;
    [ReadOnly, SerializeField] private Trap targetTrap;
    private Player player;
    private Collider curCollider;
    private Coroutine comboCoroutineInfo;

    private float gettingHitResetTime;

    private void Awake() {
        player = GetComponent<Player>();
        curCollider = GetComponent<Collider>();


        gettingHitResetTime = 0.8f;
    }

    public void EnablePlayerCollider() {
        curCollider.enabled = true;
    }

    public void SetWeapons(Weapon inputLeftWeapon, Weapon inputRightWeapon) {
        leftWeapon = inputLeftWeapon;
        rightWeapon = inputRightWeapon;
    }

    public void Attack() {
        if(!player.playerInfo.isRolling && player.playerInfo.isGrounded && !player.playerInfo.isAttacking && !player.playerInfo.isGettingHit) {
            player.playerInfo.isAttacking = true; 

            if(comboCoroutineInfo != null) 
                StopCoroutine("CheckComboLimit");
            comboCoroutineInfo = StartCoroutine("CheckComboLimit");

            player.playerAnimation.PlayAttackAnimation();

            player.playerInfo.attackIndex++;
            if(player.playerInfo.attackIndex > WeaponManager.instance.maxComboIndexOfWeapons[(int)player.playerInfo.curWeapon.weaponType]) 
                player.playerInfo.attackIndex = 0;   
        }
    }

    private IEnumerator CheckComboLimit() {
        yield return new WaitForSeconds(player.playerInfo.comboLimitTime);
        player.playerInfo.attackIndex = 0;
    }

    public void EnableLeftWeaponCollider() {
        leftWeapon.EnableCollider(true);
    }
    public void DisableLeftWeaponCollider() {
        leftWeapon.EnableCollider(false);
    }

    public void EnableRightWeaponCollider() {
        rightWeapon.EnableCollider(true);
    }
    public void DisableRightWeaponCollider() {
        rightWeapon.EnableCollider(false);
    }

    public void SetIsAttackingFalse() {
        player.playerInfo.isAttacking = false;
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "Weapon": 
            monsterWeapon = other.GetComponent<Weapon>();
            if(!player.playerInfo.isInvulnerable && monsterWeapon.owner == "Monster" && !player.playerInfo.isGettingHit)
                GetDamaged(monsterWeapon.attackPower); 
            break;
            case "Trap": 
            targetTrap = other.GetComponent<Trap>();
            if(!player.playerInfo.isInvulnerable && !player.playerInfo.isGettingHit)
                GetDamaged(targetTrap.attackPower); 
            break;
            case "DeadZone": StartCoroutine("Die"); break;
            case "Warp": 
            MySceneManager.instance.isInitial = false;
            MySceneManager.instance.isWarped = true;
            MySceneManager.instance.loadedData = new SaveData(MySceneManager.instance.curSceneName, System.DateTime.Now, Vector3.one, player.playerInfo.availableWeapons, player.playerInfo.health);
            MySceneManager.instance.LoadScene(other.GetComponent<WarpPoint>().warpDestination);
            break;
        }
    }

    private IEnumerator ResetIsGettingHitAfterSomeTime() {
        yield return new WaitForSeconds(gettingHitResetTime);
        player.playerInfo.isGettingHit = false;
    }

    private void GetDamaged(int attackPower) {
        player.playerInfo.health -= attackPower;

        if(player.playerInfo.health > 0) {
            player.playerInfo.isGettingHit = true;
            StartCoroutine("ResetIsGettingHitAfterSomeTime");

            player.playerAnimation.PlayHitAnimation();

            SetIsAttackingFalse();
            player.playerMovement.SetIsRollingFalse();
            
            UIManager.instance.UpdatePlayerHealthBar();
        }
        else
            StartCoroutine("Die");
    }

    private IEnumerator Die() {
        player.playerInfo.health = 0;
        UIManager.instance.UpdatePlayerHealthBar();
        player.playerAnimation.PlayDeathAnimation();

        curCollider.enabled = false;
        player.playerMovement.DisableGravity();
        GameManager.instance.DisablePlayerInput();
        yield return new WaitForSeconds(2.5f); // -> this one also might be changed to waituntil or so

        UIManager.instance.EnableGameOverPopup();
    }
}