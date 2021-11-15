using UnityEngine;

public class WeaponSelectionPopup : MonoBehaviour
{
    [ReadOnly] public WeaponSelectionBox[] weaponSelectionBoxes;

    private void Awake() {
        weaponSelectionBoxes = GetComponentsInChildren<WeaponSelectionBox>();
    }
}