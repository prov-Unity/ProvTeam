using UnityEngine;
using TMPro;

public class InteractionPopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private TextMeshProUGUI interactionText;

    private void Awake() {
        interactionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(GameManager.instance.player.playerInteraction.targetWeapon != null) {
                if(GameManager.instance.player.playerInteraction.isNewWeapon) {
                    GameManager.instance.player.playerInteraction.PickupNewWeapon();
                }
                else {
                    GameManager.instance.player.playerInteraction.ReplaceCurrentWeapon();
                }
            }
            else if(GameManager.instance.player.playerInteraction.targetSavePoint != null) {
                UIManager.instance.EnableSavePopup();
            }
        }    
    }

    public void SetInteractionText(string inputText) {
        interactionText.text = inputText;
    }
}