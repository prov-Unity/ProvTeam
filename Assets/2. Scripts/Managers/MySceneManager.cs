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

        switch(sceneName) {
            case "Tutorial": GameManager.instance.tutorialStartPoint = FindObjectOfType<TutorialStartPoint>().transform; break;
            case "PlayScene": GameManager.instance.stageOneStartPoint = FindObjectOfType<StageOneStartPoint>().transform; break;
        }

        if(sceneName != "MainMenu")
            GameManager.instance.InitSpawnPlayer(sceneName);
        
    }
}