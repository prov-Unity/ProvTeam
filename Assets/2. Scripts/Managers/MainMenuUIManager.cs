using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [ReadOnly] public static MainMenuUIManager instance;

    [ReadOnly, SerializeField] private LoadPopup popupLoad;

    private void Awake() {
        instance = this;

        popupLoad = GetComponentInChildren<LoadPopup>();
        popupLoad.gameObject.SetActive(false);
    }

    public void EnableLoadPopup() {
        popupLoad.gameObject.SetActive(true);
    }
}
