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
    [ReadOnly] public List<Sprite> weaponIcons;
    [ReadOnly, SerializeField] private WeaponSelectionPopup popupWeaponSelection;
    

    [ReadOnly, SerializeField] private int curSelectedWeaponIndex;

    private void Awake() {
        instance = this;

        weaponIcons = new List<Sprite>();
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Fist"));
        weaponIcons.Add(weaponIcons[(int)WeaponType.Fist_Left]);
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Bone"));

        // this code would be altered after save/load fuctionality is implemented
        // if the system forces player to start a game with fist at first, then this code good enough
        curSelectedWeaponIndex = 0;
    }

    private void Start() {
        popupWeaponSelection = UIManager.instance.popupWeaponSelection;

        UIManager.instance.DisableWeaponSelectionPopup();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            UIManager.instance.EnableWeaponSelectionPopup();
            GameManager.instance.SetTimeScale(0.01f);
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            UIManager.instance.DisableWeaponSelectionPopup();
            SelectCurrentWeapon();
            GameManager.instance.SetTimeScale(1);
        }
    }

    public void SetWeaponSelectionBox(int boxIndex, WeaponType weaponType) {
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].textWeaponName.text = weaponType.ToString().Split(new char[] {'_'})[0];
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].imageWeaponIcon.sprite = weaponIcons[(int)weaponType];
    }

    public void EnableWeaponSelectionBox(int boxIndex) {
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].gameObject.SetActive(true);
    }

    public void DisableWeaponSelectionBox(int boxIndex) {
        popupWeaponSelection.weaponSelectionBoxes[boxIndex].gameObject.SetActive(false);
    }

    public void SelectCurrentWeapon() {
        if(GameManager.instance.player.playerInfo.curWeapon != GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex]) {
            switch(GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex].weaponType) {
                case WeaponType.Fist_Left: 
                // switch to fist
                GameManager.instance.player.playerLeftWeaponSlot.SelectWeapon(WeaponType.Fist_Left);
                GameManager.instance.player.playerRightWeaponSlot.SelectWeapon(WeaponType.Fist_Right);

                GameManager.instance.player.playerCombat.SetWeapons(GameManager.instance.player.playerLeftWeaponSlot.curWeapon.GetComponent<Weapon>(), GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());

                GameManager.instance.player.playerInfo.curWeapon = GameManager.instance.player.playerInfo.availableWeapons.Find(x => x.weaponType == WeaponType.Fist_Left);
                GameManager.instance.player.playerInfo.attackIndex = 0;
                break;

                case WeaponType.Bone_Right:
                // switch to bone
                GameManager.instance.player.playerLeftWeaponSlot.DestroyCurWeapon();
                GameManager.instance.player.playerRightWeaponSlot.SelectWeapon(WeaponType.Bone_Right);
                GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>().durability = GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex].durability;
                
                GameManager.instance.player.playerCombat.SetWeapons(null, GameManager.instance.player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());

                GameManager.instance.player.playerInfo.curWeapon = GameManager.instance.player.playerInfo.availableWeapons.Find(x => x.weaponType == WeaponType.Bone_Right);
                GameManager.instance.player.playerInfo.attackIndex = 0;
                break;
            }
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
        SetWeaponSelectionBox(2, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex].weaponType);

        // update two left weapon selection boxes
        if(curSelectedWeaponIndex == 1) {
            DisableWeaponSelectionBox(0);

            EnableWeaponSelectionBox(1);
            SetWeaponSelectionBox(1, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex - 1].weaponType);
        }
        else if(curSelectedWeaponIndex >= 2) {
            EnableWeaponSelectionBox(0);
            SetWeaponSelectionBox(0, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex - 2].weaponType);

            EnableWeaponSelectionBox(1);
            SetWeaponSelectionBox(1, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex - 1].weaponType);
        }
        else {
            DisableWeaponSelectionBox(0);
            DisableWeaponSelectionBox(1);
        }

        // update two right weapon selection boxes
        if((GameManager.instance.player.playerInfo.availableWeapons.Count - curSelectedWeaponIndex) == 2) {
            EnableWeaponSelectionBox(3);
            SetWeaponSelectionBox(3, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex + 1].weaponType);

            DisableWeaponSelectionBox(4);
        }
        else if((GameManager.instance.player.playerInfo.availableWeapons.Count - curSelectedWeaponIndex) >= 3) {
            EnableWeaponSelectionBox(3);
            SetWeaponSelectionBox(3, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex + 1].weaponType);

            EnableWeaponSelectionBox(4);
            SetWeaponSelectionBox(4, GameManager.instance.player.playerInfo.availableWeapons[curSelectedWeaponIndex + 2].weaponType);
        }
        else {
            DisableWeaponSelectionBox(3);
            DisableWeaponSelectionBox(4);
        }
    }
}