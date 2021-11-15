using UnityEngine;

public class WeaponSelectionPopup : MonoBehaviour
{
    [ReadOnly] public WeaponSelectionBox[] weaponSelectionBoxes;

    private void Awake() {
        weaponSelectionBoxes = GetComponentsInChildren<WeaponSelectionBox>();
    }

    private void Update() {
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            WeaponSelectionManager.instance.MoveWeaponSelectionBoxesLeftOnce();

        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            WeaponSelectionManager.instance.MoveWeaponSelectionBoxesRightOnce();
    }
}