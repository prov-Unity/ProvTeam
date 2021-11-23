using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelectionBox : MonoBehaviour
{
    [ReadOnly, SerializeField] private UIWeaponInfo weaponInfo;
    [ReadOnly, SerializeField] private TextMeshProUGUI textWeaponName;
    [ReadOnly, SerializeField] private Image imageWeaponIcon;

    private void Awake() {
        weaponInfo = GetComponentInChildren<UIWeaponInfo>();
        textWeaponName = GetComponentsInChildren<TextMeshProUGUI>()[1];
        imageWeaponIcon = GetComponentsInChildren<Image>()[3];
    }

    public void UpdateWeaponSelectionBox(string inputTextWeaponName, Sprite inputImageWeaponIcon) {
        textWeaponName.text = inputTextWeaponName;
        imageWeaponIcon.sprite = inputImageWeaponIcon;
    }

    public void UpdateWeaponInfo(AvailableWeapon targetWeapon) {
        weaponInfo.UpdateWeaponInfo(targetWeapon);
    } 

    public void EnableWeaponInfo() {
        weaponInfo.gameObject.SetActive(true);
    }

    public bool IsWeaponInfoActive() {
        return weaponInfo.gameObject.activeInHierarchy;
    }

    public void DisableWeaponInfo() {
        weaponInfo.gameObject.SetActive(false);
    }
}