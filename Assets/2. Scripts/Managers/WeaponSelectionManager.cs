using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour
{
    public static WeaponSelectionManager instance;
    [ReadOnly, SerializeField] private WeaponSelectionPopup popupWeaponSelection;
    [ReadOnly, SerializeField] private List<Sprite> weaponIcons;

    private void Awake() {
        instance = this;

        weaponIcons = new List<Sprite>();
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Fist"));
        weaponIcons.Add(weaponIcons[(int)WeaponType.Fist_Left]);
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Bone"));
    }

    private void Start() {
        popupWeaponSelection = UIManager.instance.popupWeaponSelection;

        // these codes would be altered after save/load functionality is implemented
        DisableWeaponSelectionBox(0);
        DisableWeaponSelectionBox(1);
        DisableWeaponSelectionBox(4);

        SetWeaponSelectionBox(2, WeaponType.Fist_Left);
        SetWeaponSelectionBox(3, WeaponType.Bone_Right);

        UIManager.instance.DisableWeaponSelectionPopup();
    }

    public void SetWeaponSelectionBox(int boxIndex, WeaponType weaponType) {
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].textWeaponName.text = Regex.Split(weaponType.ToString(), "_")[0];
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].imageWeaponIcon.sprite = weaponIcons[(int)weaponType];
    }

    public void EnableWeaponSelectionBox(int boxIndex) {
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].gameObject.SetActive(true);
    }

    public void DisableWeaponSelectionBox(int boxIndex) {
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].gameObject.SetActive(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            UIManager.instance.EnableWeaponSelectionPopup();
            GameManager.instance.SetTimeScale(0.1f);
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            UIManager.instance.DisableWeaponSelectionPopup();
            GameManager.instance.SetTimeScale(1);
        }
    }
}