using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPopup : MonoBehaviour
{
    [ReadOnly] private TextMeshProUGUI interactionText;

    private void Awake() {
        interactionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E) && GameManager.instance.player.playerInteractionManager.targetWeapon != null) {
            if(GameManager.instance.player.playerInteractionManager.isNewWeapon) {
                // code for picking up new weapon
                GameManager.instance.player.playerInteractionManager.PickupNewWeapon();
            }
            else {
                // code for replacing existing weapon
                GameManager.instance.player.playerInteractionManager.ReplaceCurrentWeapon();
            }
        }
    }

    public void SetInteractionText(string inputText) {
        interactionText.text = inputText;
    }
}