using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [ReadOnly] public static MySceneManager instance;
    [ReadOnly] public SaveData loadedData;

    [ReadOnly] public string curSceneName;
    [ReadOnly] public bool isInitial;

    private void Awake() {
        instance = this;
        curSceneName = "MainMenu";

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName) {
        curSceneName = sceneName;
        loadedData = SaveLoadManager.instance.GetLatestData();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}