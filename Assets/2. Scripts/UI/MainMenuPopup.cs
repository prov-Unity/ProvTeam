using UnityEngine;

public class MainMenuPopup : MonoBehaviour
{
    public void OnClickStart() {
        MySceneManager.instance.isInitial = true;
        MySceneManager.instance.LoadScene("Tutorial");
    }

    public void OnClickLoad() {
        MainMenuUIManager.instance.EnableLoadPopup();
    }
}