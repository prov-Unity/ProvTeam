using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour
{
    /*
    How does weapon selection system work?

    based on user's input, curBoxIndex is changed
    curBoxIndex's range is the same as the one of Player's availableWeapons
    based on curBoxIndex, the actual weapon selection box would be changed
    after user relases tab button, then the weapon selection box at the index of 2 of weapon_selection_boxes would be regarded as the weapon which user picked
    hence, this manager would change player's weapon to the one at the index of 2 
    */
    public static WeaponSelectionManager instance;
    [ReadOnly, SerializeField] private WeaponSelectionPopup popupWeaponSelection;
    

    [ReadOnly, SerializeField] private int curSelectedWeaponIndex;

    private void Awake() {
        instance = this;

        curSelectedWeaponIndex = 0;
    }

    private void Start() {
        popupWeaponSelection = UIManager.instance.popupWeaponSelection;

        UIManager.instance.DisableWeaponSelectionPopup();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab) && !GameManager.instance.player.playerInfo.isGettingHit) {
            UIManager.instance.EnableWeaponSelectionPopup();
            GameManager.instance.SetTimeScale(0.01f);
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            UIManager.instance.DisableWeaponSelectionPopup();
            SelectCurrentWeapon();
            GameManager.instance.SetTimeScale(1);
        }
    }

    public void ResetSelectedWeaponIndex() {
        curSelectedWeaponIndex = 0;
    }

    public void SetWeaponSelectionBox(int boxIndex, AvailableWeapon targetWeapon) {
        if(popupWeaponSelection != null) {
            if(targetWeapon.weaponType == WeaponType.Fist_Left) {
                if(popupWeaponSelection.weaponSelectionBoxes[boxIndex].IsWeaponInfoActive()) 
                    popupWeaponSelection.weaponSelectionBoxes[boxIndex].DisableWeaponInfo();
            }
            else {
                if(popupWeaponSelection.weaponSelectionBoxes[boxIndex].IsWeaponInfoActive() == false) 
                    popupWeaponSelection.weaponSelectionBoxes[boxIndex].EnableWeaponInfo();

                popupWeaponSelection.weaponSelectionBoxes[boxIndex].UpdateWeaponInfo(targetWeapon);
            }

            popupWeaponSelection.weaponSelectionBoxes[boxIndex].UpdateWeaponSelectionBox(targetWeapon.weaponType.ToString().Split(new char[] {'_'})[0], WeaponManager.instance.weaponIcons[(int)targetWeapon.weaponType]);
        }
    }

    public void EnableWeaponSelectionBox(int boxIndex) {
        if(popupWeaponSelection != null)
            popupWeaponSelection.weaponSelectionBoxes[boxIndex].gameObject.SetActive(true);
    }

    public void DisableWeaponSelectionBox(int boxIndex) {
        if(popupWeaponSelection != null)
            popupWeaponSelection.weaponSelectionBoxes[boxIndex].gameObject.SetActive(false);
    }

    public void SelectCurrentWeapon() {
        if(GameManager.instance.player.playerInfo.curWeapon != GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex]) {
            WeaponType curWeaponType = GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex].weaponType;
            if(curWeaponType == WeaponType.Fist_Left) {
                GameManager.instance.player.playerLeftWeaponSlot.SelectWeapon(WeaponType.Fist_Left);
                GameManager.instance.player.playerRightWeaponSlot.SelectWeapon(WeaponType.Fist_Right);

                GameManager.instance.player.playerCombat.SetWeapons(GameManager.instance.player.playerLeftWeaponSlot.curWeapon.GetComponent<Weapon>(), GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());
            }
            else {
                GameManager.instance.player.playerLeftWeaponSlot.DestroyCurWeapon();
                GameManager.instance.player.playerRightWeaponSlot.SelectWeapon(curWeaponType);

                GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>().durability = GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex].durability;
                GameManager.instance.player.playerCombat.SetWeapons(null, GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());
            }
            GameManager.instance.player.playerInfo.curWeapon = GameManager.instance.player.playerInfo.availableWeapons.Find(x => x.weaponType == curWeaponType);
            GameManager.instance.player.playerInfo.attackIndex = 0;

            GameManager.instance.player.playerAnimation.UpdateCurWeaponIndex();
            UIManager.instance.UpdateCurWeaponInfo();
        }
    }

    public void MoveWeaponSelectionBoxesLeftOnce() {
        curSelectedWeaponIndex++;
        if(curSelectedWeaponIndex >= GameManager.instance.player.playerInfo.availableWeapons.Count)
            curSelectedWeaponIndex = (GameManager.instance.player.playerInfo.availableWeapons.Count - 1);
        else 
            UpdateWeaponSelectionBox();
    }

    public void MoveWeaponSelectionBoxesRightOnce() {
        curSelectedWeaponIndex--;
        if(curSelectedWeaponIndex < 0)
            curSelectedWeaponIndex = 0;
        else
            UpdateWeaponSelectionBox();
    }

    public void UpdateWeaponSelectionBox() {
        // update currently selected weapon
        SetWeaponSelectionBox(2, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex]);

        // update two left weapon selection boxes
        if(curSelectedWeaponIndex == 1) {
            DisableWeaponSelectionBox(0);

            EnableWeaponSelectionBox(1);
            SetWeaponSelectionBox(1, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex - 1]);
        }
        else if(curSelectedWeaponIndex >= 2) {
            EnableWeaponSelectionBox(0);
            SetWeaponSelectionBox(0, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex - 2]);

            EnableWeaponSelectionBox(1);
            SetWeaponSelectionBox(1, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex - 1]);
        }
        else {
            DisableWeaponSelectionBox(0);
            DisableWeaponSelectionBox(1);
        }

        // update two right weapon selection boxes
        if((GameManager.instance.player.playerInfo.availableWeapons.Count - curSelectedWeaponIndex) == 2) {
            EnableWeaponSelectionBox(3);
            SetWeaponSelectionBox(3, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex + 1]);

            DisableWeaponSelectionBox(4);
        }
        else if((GameManager.instance.player.playerInfo.availableWeapons.Count - curSelectedWeaponIndex) >= 3) {
            EnableWeaponSelectionBox(3);
            SetWeaponSelectionBox(3, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex + 1]);

            EnableWeaponSelectionBox(4);
            SetWeaponSelectionBox(4, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex + 2]);
        }
        else {
            DisableWeaponSelectionBox(3);
            DisableWeaponSelectionBox(4);
        }
    }
}