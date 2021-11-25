using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public void OnClickRestart() {
        GameManager.instance.RespawnPlayer();
        UIManager.instance.DisableGameOverPopup();
    }

    public void OnClickMainMenu() {
        // do something to load main menu
    }
}