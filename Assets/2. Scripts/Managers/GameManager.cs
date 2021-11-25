using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [ReadOnly] public static GameManager instance;
    [ReadOnly] public Player player;
    public Transform initSpawnPoint;

    private void Awake() {
        instance = this;
        player = FindObjectOfType<Player>();

        //initSpawnPoint = FindObjectOfType<>();
    }

    public void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * timeScale;
    }

    public void EnablePlayerInput() {
        player.playerInput.enabled = true;
    }

    public void DisablePlayerInput() {
        player.playerInput.enabled = false;
    }

    // public void EnablePlayer() {
    //     player.gameObject.SetActive(true);
    // }

    // public void DisablePlayer() {
    //     player.gameObject.SetActive(false);
    // }

    public void InitSpawnPlayer() {

    }

    public void RespawnPlayer() {
        player.playerCombat.EnablePlayerCollider();
        player.playerMovement.EnableGravity();
        EnablePlayerInput();

        SaveData latestData = SaveLoadManager.instance.GetLatestData();
        player.transform.position = latestData.respawnPoint;
        player.playerInfo.health = latestData.savedHealth;
        player.playerInfo.availableWeapons = latestData.savedAvailableWeapons;
        UIManager.instance.UpdatePlayerHealthBar();

        player.playerInfo.isAttacking = false;

        // force the player to select fist right after being respawned
        UIManager.instance.EnableWeaponSelectionPopup();
        WeaponSelectionManager.instance.ResetSelectedWeaponIndex();
        WeaponSelectionManager.instance.UpdateWeaponSelectionBox();
        WeaponSelectionManager.instance.SelectCurrentWeapon();
        UIManager.instance.DisableWeaponSelectionPopup();
    }
}