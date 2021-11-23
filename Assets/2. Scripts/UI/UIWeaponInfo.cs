using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWeaponInfo : MonoBehaviour
{
    [ReadOnly] public TextMeshProUGUI textWeaponPower;
    [ReadOnly] public Image imageWeaponDurability;
    private void Awake() {
        textWeaponPower = GetComponentInChildren<TextMeshProUGUI>();
        imageWeaponDurability = GetComponentsInChildren<Image>()[1];
    }

    public void UpdateWeaponInfo(AvailableWeapon targetWeapon) {
        textWeaponPower.text = "Power: " + WeaponManager.instance.weaponAttackPowers[(int)targetWeapon.weaponType].ToString();

        imageWeaponDurability.fillAmount = (float)targetWeapon.durability / WeaponManager.instance.weaponInitialDurabilities[(int)targetWeapon.weaponType];
    }
}