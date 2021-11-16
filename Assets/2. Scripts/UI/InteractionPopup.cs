using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPopup : MonoBehaviour
{
    [ReadOnly] private TextMeshProUGUI interactionText;

    private void Awake() {
        interactionText = GetComponent<TextMeshProUGUI>();
    }
}