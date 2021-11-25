using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [ReadOnly] public static UIManager instance;
    [ReadOnly] public WeaponSelectionPopup popupWeaponSelection;

    [ReadOnly] public bool isInteractionPopupDisabled;


    [ReadOnly, SerializeField] private Image playerHealthBar;
    [ReadOnly, SerializeField] private UIMonsterInfo monsterInfo;
    [ReadOnly, SerializeField] private UICurWeaponInfo curWeaponInfo;

    [ReadOnly, SerializeField] private MainMenuPopup popupMainMenu;
    [ReadOnly, SerializeField] private InteractionPopup popupInteraction;
    [ReadOnly, SerializeField] private SavePopup popupSave;
    [ReadOnly, SerializeField] private GameOverPopup popupGameOver;

    private void Awake()
    {        
        instance = this;

        playerHealthBar = FindObjectOfType<PlayerHealthBar>().GetComponent<Image>();
        monsterInfo = FindObjectOfType<UIMonsterInfo>();
        curWeaponInfo = FindObjectOfType<UICurWeaponInfo>();

        popupWeaponSelection = FindObjectOfType<WeaponSelectionPopup>();
        popupMainMenu = FindObjectOfType<MainMenuPopup>();
        popupInteraction = FindObjectOfType<InteractionPopup>();
        popupInteraction.gameObject.SetActive(false);
        popupSave = FindObjectOfType<SavePopup>();
        popupSave.gameObject.SetActive(false);
        popupGameOver = FindObjectOfType<GameOverPopup>();
        popupGameOver.gameObject.SetActive(false);

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

    public void EnableMainMenuPopup() {
        popupMainMenu.gameObject.SetActive(true);
    }
    public void DisableMainMenuPopup() {
        popupMainMenu.gameObject.SetActive(false);
    }


    public void EnableWeaponSelectionPopup() {
        popupWeaponSelection.gameObject.SetActive(true);
        GameManager.instance.DisablePlayerInput();
    }

    public void DisableWeaponSelectionPopup() {
        popupWeaponSelection.gameObject.SetActive(false);
        GameManager.instance.EnablePlayerInput();
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

    public void EnableSavePopup() {
        GameManager.instance.DisablePlayerInput();
        GameManager.instance.SetTimeScale(0.01f);
        popupSave.gameObject.SetActive(true);
        popupSave.LoadFiles();
    }

    public void EnableGameOverPopup() {
        GameManager.instance.DisablePlayerInput();
        GameManager.instance.SetTimeScale(0);
        popupGameOver.gameObject.SetActive(true);
    }

    public void DisableGameOverPopup() {
        GameManager.instance.EnablePlayerInput();
        GameManager.instance.SetTimeScale(1f);
        popupGameOver.gameObject.SetActive(false);
    }
}