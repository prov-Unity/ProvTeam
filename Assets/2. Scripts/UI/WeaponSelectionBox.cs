using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelectionBox : MonoBehaviour
{
    [ReadOnly] public TextMeshProUGUI textWeaponName;
    [ReadOnly] public Image imageWeaponIcon;

    private void Awake() {
        textWeaponName = GetComponentInChildren<TextMeshProUGUI>();
        imageWeaponIcon = GetComponentsInChildren<Image>()[1];
    }
}