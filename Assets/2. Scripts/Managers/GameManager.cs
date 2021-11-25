using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [ReadOnly] public static GameManager instance;
    [ReadOnly] public Player player;
    [ReadOnly] public Transform tutorialStartPoint;
    [ReadOnly] public Transform stageOneStartPoint;

    [ReadOnly] public GameObject playerAndCameraPrefab;

    private void Awake() {
        instance = this;
        player = FindObjectOfType<Player>();

        playerAndCameraPrefab = Resources.Load<GameObject>("Player_And_Camera");
    }

    private void Start() {
        if(MySceneManager.instance.curSceneName != "MainMenu") {
            InitSpawnPlayer(MySceneManager.instance.curSceneName);
        }
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

    public void InitSpawnPlayer(string sceneName) {
        switch(sceneName) {
            case "Tutorial": tutorialStartPoint = FindObjectOfType<TutorialStartPoint>().transform; break;
            case "PlayScene": stageOneStartPoint = FindObjectOfType<StageOneStartPoint>().transform; break;
        }

        if(player != null) {}
            Destroy(player.GetComponentsInParent<Transform>()[1].gameObject);
        GameObject instantiatedPlayerAndCameraObject = Instantiate(playerAndCameraPrefab);
        player = instantiatedPlayerAndCameraObject.GetComponentInChildren<Player>();

        switch(sceneName) {
            case "Tutorial": player.transform.position = tutorialStartPoint.position; break;
            case "PlayScene": player.transform.position = stageOneStartPoint.position; break;
        }
        player.playerInfo.health = 100;
        player.playerInfo.availableWeapons = new List<AvailableWeapon>();
        player.playerInfo.availableWeapons.Add(new AvailableWeapon(WeaponType.Fist_Left, WeaponManager.instance.weaponInitialDurabilities[(int)WeaponType.Fist_Left]));
        player.playerInfo.curWeapon = player.playerInfo.availableWeapons[0];

        SaveLoadManager.instance.SaveData(0, new SaveData(sceneName, DateTime.Now, player.transform.position, player.playerInfo.availableWeapons, player.playerInfo.health));
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