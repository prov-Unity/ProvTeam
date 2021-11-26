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
            if(MySceneManager.instance.isInitial && MySceneManager.instance.curSceneName == "Tutorial")
                InitSpawnPlayer();
            else
                SpawnPlayer(MySceneManager.instance.curSceneName, MySceneManager.instance.loadedData);
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

    public void InitSpawnPlayer() {
        Destroy(player.GetComponentsInParent<Transform>()[1].gameObject);
        GameObject instantiatedPlayerAndCameraObject = Instantiate(playerAndCameraPrefab);
        player = instantiatedPlayerAndCameraObject.GetComponentInChildren<Player>();
        

        tutorialStartPoint = FindObjectOfType<TutorialStartPoint>().transform;
        player.transform.position = tutorialStartPoint.position;


        player.playerInfo.health = 100;
        player.playerInfo.availableWeapons = new List<AvailableWeapon>();
        player.playerInfo.availableWeapons.Add(new AvailableWeapon(WeaponType.Fist_Left, WeaponManager.instance.weaponInitialDurabilities[(int)WeaponType.Fist_Left]));
        player.playerInfo.curWeapon = new AvailableWeapon(WeaponType.No_Weapon, -1);
        WeaponSelectionManager.instance.SelectCurrentWeapon();
        player.playerInfo.curWeapon = player.playerInfo.availableWeapons[0];


        SaveLoadManager.instance.SaveData(0, new SaveData("Tutorial", DateTime.Now, player.transform.position, player.playerInfo.availableWeapons, player.playerInfo.health));
    }

    public void SpawnPlayer(string sceneName, SaveData loadedData) {
        Destroy(player.GetComponentsInParent<Transform>()[1].gameObject);
        GameObject instantiatedPlayerAndCameraObject = Instantiate(playerAndCameraPrefab);
        player = instantiatedPlayerAndCameraObject.GetComponentInChildren<Player>();

        if(MySceneManager.instance.isInitial) {
            stageOneStartPoint = FindObjectOfType<StageOneStartPoint>().transform;
            player.transform.position = stageOneStartPoint.position;
        }
        else
            player.transform.position = loadedData.respawnPoint;

        player.playerInfo.health = loadedData.savedHealth;
        player.playerInfo.availableWeapons = new List<AvailableWeapon>();
        foreach(AvailableWeapon curWeapon in loadedData.savedAvailableWeapons) {
            player.playerInfo.availableWeapons.Add(curWeapon);
        }
        player.playerInfo.curWeapon = new AvailableWeapon(WeaponType.Fist_Left, -1);
        WeaponSelectionManager.instance.SelectCurrentWeapon();
        player.playerInfo.curWeapon = player.playerInfo.availableWeapons[0];
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