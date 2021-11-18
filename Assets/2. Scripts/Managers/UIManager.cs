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


    public void UpdatePlayerHealthBar(int inputPlayerHealth)
    {
        playerHealthBar.fillAmount = (float)inputPlayerHealth/100f;
    }

    public void EnableMonsterInfo() {
        monsterInfo.gameObject.SetActive(true);
    }

    public void UpdateMonsterInfo(string inputMonsterName, int inputMonsterHealth, int monsterMaxHealth) {
        monsterInfo.textMonsterName.text = inputMonsterName;
        monsterInfo.imageMonsterHealth.fillAmount = (float)inputMonsterHealth/monsterMaxHealth;
    }

    public void DisableMonsterInfo() {
        monsterInfo.gameObject.SetActive(false);
    }


    public void UpdateCurWeaponInfo() {
        if(GameManager.instance.player.playerInfo.curWeapon.weaponType == WeaponType.Fist_Left)
            curWeaponInfo.textWeaponDurability.text = "";
        else
            curWeaponInfo.textWeaponDurability.text = GameManager.instance.player.playerInfo.curWeapon.durability.ToString();

        curWeaponInfo.imageWeaponIcon.sprite = WeaponSelectionManager.instance.weaponIcons[(int)GameManager.instance.player.playerInfo.curWeapon.weaponType];
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