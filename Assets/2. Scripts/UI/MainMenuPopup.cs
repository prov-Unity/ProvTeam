using UnityEngine;

public class MainMenuPopup : MonoBehaviour
{
    public void OnClickStart() {
        // do something to start the new game
        MySceneManager.instance.LoadScene("Tutorial");
        MySceneManager.instance.isInitial = true;
    }

    public void OnClickLoad() {
        // do something to load main menu
        MySceneManager.instance.isInitial = false;
    }
}