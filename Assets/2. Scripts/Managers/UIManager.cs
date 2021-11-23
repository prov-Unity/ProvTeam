using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [ReadOnly] public static UIManager instance;

    [ReadOnly, SerializeField] private Image playerHealthBar;
    [ReadOnly, SerializeField] private UIMonsterInfo monsterInfo;
    [ReadOnly, SerializeField] private UICurWeaponInfo curWeaponInfo;


    [ReadOnly] public WeaponSelectionPopup popupWeaponSelection;
    [ReadOnly] private InteractionPopup popupInteraction;

    [ReadOnly] public bool isInteractionPopupDisabled;

    private void Awake()
    {        
        instance = this;

        playerHealthBar = FindObjectOfType<PlayerHealthBar>().GetComponent<Image>();
        monsterInfo = FindObjectOfType<UIMonsterInfo>();
        curWeaponInfo = FindObjectOfType<UICurWeaponInfo>();

        popupWeaponSelection = FindObjectOfType<WeaponSelectionPopup>();
        popupInteraction = FindObjectOfType<InteractionPopup>();
        popupInteraction.gameObject.SetActive(false);

        isInteractionPopupDisabled = true;
    }

    private void Start() {
        monsterInfo.gameObject.SetActive(false);
    }


    public void UpdatePlayerHealthBar()
    {
        playerHealthBar.fillAmount = GameManager.instance.player.playerInfo.health/100f;
    }

    public void EnableMonsterInfo() {
        monsterInfo.gameObject.SetActive(true);
    }

    public void UpdateMonsterInfo(MonsterAI targetMonster) {
        monsterInfo.UpdateMonsterInfo(targetMonster);
    }

    public void DisableMonsterInfo() {
        monsterInfo.gameObject.SetActive(false);
    }


    public void UpdateCurWeaponInfo() {
        if(GameManager.instance.player.playerInfo.curWeapon.weaponType == WeaponType.Fist_Left) {
            if(curWeaponInfo.IsWeaponDurabilityActive()) 
                curWeaponInfo.DisableWeaponDurability();
        }
        else {
            if(curWeaponInfo.IsWeaponDurabilityActive() == false) 
                curWeaponInfo.EnableWeaponDurability();

            curWeaponInfo.UpdateWeaponDurability(GameManager.instance.player.playerInfo.curWeapon);
        }
        curWeaponInfo.UpdatecurWeaponIcon(WeaponSelectionManager.instance.weaponIcons[(int)GameManager.instance.player.playerInfo.curWeapon.weaponType]);
    }


    public void EnableWeaponSelectionPopup() {
        popupWeaponSelection.gameObject.SetActive(true);
    }

    public void DisableWeaponSelectionPopup() {
        popupWeaponSelection.gameObject.SetActive(false);
    }


    public void EnableInteractionPopup() {
        popupInteraction.gameObject.SetActive(true);
        isInteractionPopupDisabled = false;
    }

    public void SetInteractionPopupText(string inputText) {
        popupInteraction.SetInteractionText(inputText);
    }

    public void DisableInteractionPopup() {
        popupInteraction.gameObject.SetActive(false);
        isInteractionPopupDisabled = true;
    }
}