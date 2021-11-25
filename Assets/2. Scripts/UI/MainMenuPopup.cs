using UnityEngine;

public class MainMenuPopup : MonoBehaviour
{
    public void OnClickStart() {
        // do something to start the new game
        MySceneManager.instance.LoadScene("Tutorial");
    }

    public void OnClickLoad() {
        // do something to load main menu

    }
}