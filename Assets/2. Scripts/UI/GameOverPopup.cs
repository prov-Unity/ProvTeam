using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public void OnClickRestart() {
        GameManager.instance.RespawnPlayer();
        UIManager.instance.DisableGameOverPopup();
    }

    public void OnClickMainMenu() {
        Destroy(MySceneManager.instance.gameObject);
        GameManager.instance.SetTimeScale(1f);
        MySceneManager.instance.LoadScene("MainMenu");
    }
}