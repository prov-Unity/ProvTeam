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

        tutorialStartPoint = FindObjectOfType<TutorialStartPoint>().transform;
        stageOneStartPoint = FindObjectOfType<StageOneStartPoint>().transform;

        playerAndCameraPrefab = Resources.Load<GameObject>("Player_And_Camera");
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

    public void InitSpawnPlayer(string stageName) {
        if(player != null) {}
            Destroy(player);
        GameObject instantiatedPlayerAndCameraObject = Instantiate(playerAndCameraPrefab);
        player = instantiatedPlayerAndCameraObject.GetComponentInChildren<Player>();

        switch(stageName) {
            case "Tutorial": player.transform.position = tutorialStartPoint.position; break;
            case "PlayScene": player.transform.position = stageOneStartPoint.position; break;
        }
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