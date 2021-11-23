using UnityEngine;
using TMPro;

public class InteractionPopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private TextMeshProUGUI interactionText;

    private void Awake() {
        interactionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E) && GameManager.instance.player.playerInteractionManager.targetWeapon != null) {
            if(GameManager.instance.player.playerInteractionManager.isNewWeapon) {
                GameManager.instance.player.playerInteractionManager.PickupNewWeapon();
            }
            else {
                GameManager.instance.player.playerInteractionManager.ReplaceCurrentWeapon();
            }
        }
    }

    public void SetInteractionText(string inputText) {
        interactionText.text = inputText;
    }
}