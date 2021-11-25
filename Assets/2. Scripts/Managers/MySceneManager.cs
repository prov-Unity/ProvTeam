using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;
    [ReadOnly] public string curSceneName;
    [ReadOnly] public bool isInitial;

    private void Awake() {
        instance = this;
        curSceneName = "MainMenu";

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName) {
        curSceneName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}