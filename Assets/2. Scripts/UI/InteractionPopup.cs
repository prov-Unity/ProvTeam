using UnityEngine;
using TMPro;

public class InteractionPopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private TextMeshProUGUI interactionText;

    private void Awake() {
        interactionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E) && GameManager.instance.player.playerInteraction.targetWeapon != null) {
            if(GameManager.instance.player.playerInteraction.isNewWeapon) {
                GameManager.instance.player.playerInteraction.PickupNewWeapon();
            }
            else {
                GameManager.instance.player.playerInteraction.ReplaceCurrentWeapon();
            }
        }
    }

    public void SetInteractionText(string inputText) {
        interactionText.text = inputText;
    }
}