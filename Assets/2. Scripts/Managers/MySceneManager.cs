using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    private void Awake() {
        instance = this;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        if(sceneName != "MainMenu")
            GameManager.instance.InitSpawnPlayer(sceneName);
    }
}