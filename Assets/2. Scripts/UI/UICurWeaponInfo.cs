using UnityEngine;
using UnityEngine.UI;

public class UICurWeaponInfo : MonoBehaviour
{
    [ReadOnly, SerializeField] private Image imageWeaponDurabilityBackground;
    [ReadOnly, SerializeField] private Image imageWeaponDurability;
    
    [ReadOnly, SerializeField] private Image imageWeaponIcon;

    private void Awake() {
        Image[] images = GetComponentsInChildren<Image>();

        imageWeaponDurabilityBackground = images[0];
        imageWeaponDurability = images[1];
        imageWeaponIcon = images[3];
    }

    public void UpdatecurWeaponIcon(Sprite inputImageWeaponIcon) {
        imageWeaponIcon.sprite = inputImageWeaponIcon;
    }

    public void EnableWeaponDurability() {
        imageWeaponDurabilityBackground.gameObject.SetActive(true);
        imageWeaponDurability.gameObject.SetActive(true);
    }

    public bool IsWeaponDurabilityActive() {
        return imageWeaponDurability.gameObject.activeInHierarchy;
    }

    public void UpdateWeaponDurability(AvailableWeapon curWeapon) {
        imageWeaponDurability.fillAmount = (float)curWeapon.durability / WeaponManager.instance.weaponInitialDurabilities[(int)curWeapon.weaponType];
    }

    public void DisableWeaponDurability() {
        imageWeaponDurabilityBackground.gameObject.SetActive(false);
        imageWeaponDurability.gameObject.SetActive(false);
    }
}