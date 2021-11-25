using UnityEngine;

public class MainMenuPopup : MonoBehaviour
{
    public void OnClickStart() {
        MySceneManager.instance.LoadScene("Tutorial");
        MySceneManager.instance.isInitial = true;
    }

    public void OnClickLoad() {
        SaveData latestData = SaveLoadManager.instance.GetLatestData();
        MySceneManager.instance.LoadScene(latestData.stageName);
        MySceneManager.instance.isInitial = false;
    }
}