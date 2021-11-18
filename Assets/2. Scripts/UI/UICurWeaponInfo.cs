using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICurWeaponInfo : MonoBehaviour
{
    [ReadOnly] public TextMeshProUGUI textWeaponDurability;
    [ReadOnly] public Image imageWeaponIcon;

    private void Awake() {
        textWeaponDurability = GetComponentInChildren<TextMeshProUGUI>();
        imageWeaponIcon = GetComponentsInChildren<Image>()[1];
    }
}