using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [ReadOnly] public static MySceneManager instance;
    [ReadOnly] public SaveData loadedData;

    [ReadOnly] public string curSceneName;
    [ReadOnly] public bool isInitial;
    [ReadOnly] public bool isWarped;

    private void Awake() {
        instance = this;
        curSceneName = "MainMenu";

        DontDestroyOnLoad(gameObject);

        isInitial = true;
        isWarped = false;
    }

    public void LoadScene(string sceneName) {
        curSceneName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}