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

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            UIManager.instance.EnableWeaponSelectionPopup();
            GameManager.instance.SetTimeScale(0.1f);
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            UIManager.instance.DisableWeaponSelectionPopup();
            SelectCurrentWeapon();
            GameManager.instance.SetTimeScale(1);
        }
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

    public void SelectCurrentWeapon() {
        switch(popupWeaponSelection.weaponSelectionBoxes[2].textWeaponName.text) {
            case "Fist":
            if(GameManager.instance.player.playerInfo.curWeapon != WeaponType.Fist_Left) {
                // switch to fist
                GameManager.instance.player.playerLeftWeaponSlot.SelectWeapon(WeaponType.Fist_Left);
                GameManager.instance.player.playerRightWeaponSlot.SelectWeapon(WeaponType.Fist_Right);

                GameManager.instance.player.playerCombat.SetWeapons(GameManager.instance.player.playerLeftWeaponSlot.curWeapon.GetComponent<Weapon>(), GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());

                GameManager.instance.player.playerInfo.curWeapon = WeaponType.Fist_Left;
                GameManager.instance.player.playerInfo.attackIndex = 0;
                GameManager.instance.player.playerAnimation.ChangeMoveToFist();
            }
            break;
            case "Bone": 
            if(GameManager.instance.player.playerInfo.curWeapon != WeaponType.Bone_Right) {
                // switch to bone
                GameManager.instance.player.playerLeftWeaponSlot.DestroyCurWeapon();
                GameManager.instance.player.playerRightWeaponSlot.SelectWeapon(WeaponType.Bone_Right);

                GameManager.instance.player.playerCombat.SetWeapons(null, GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());

                GameManager.instance.player.playerInfo.curWeapon = WeaponType.Bone_Right;
                GameManager.instance.player.playerInfo.attackIndex = 0;
                GameManager.instance.player.playerAnimation.ChangeMoveTo2Hand();
            }
            break;
        }
    }

    public void MoveWeaponSelectionBoxesLeftOnce() {
        Debug.Log("move left");
    }

    public void MoveWeaponSelectionBoxesRightOnce() {
        Debug.Log("move right");
    }
}