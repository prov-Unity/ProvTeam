using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [ReadOnly] public static UIManager instance;

    [ReadOnly, SerializeField] private Image playerHealthBar;
    [ReadOnly, SerializeField] private Image monsterHealthBar;

    [ReadOnly] public WeaponSelectionPopup popupWeaponSelection;
    [ReadOnly] private InteractionPopup popupInteraction;

    [ReadOnly] public bool isInteractionPopupDisabled;

    private void Awake()
    {        
        instance = this;

        playerHealthBar = FindObjectOfType<PlayerHealthBar>().GetComponent<Image>();
        monsterHealthBar = FindObjectOfType<MonsterHealthBar>().GetComponent<Image>();
        monsterHealthBar.gameObject.SetActive(false);

        popupWeaponSelection = FindObjectOfType<WeaponSelectionPopup>();
        popupInteraction = FindObjectOfType<InteractionPopup>();
        popupInteraction.gameObject.SetActive(false);

        isInteractionPopupDisabled = true;
    }

    public void UpdatePlayerHealthBar(int inputPlayerHealth)
    {
        playerHealthBar.fillAmount = (float)inputPlayerHealth/100f;
    }

    public void EnableMonsterHealthBar() {
        monsterHealthBar.gameObject.SetActive(true);
    }

    public void UpdateMonsterHealthBar(int inputMonsterHealth, int monsterMaxHealth) {
        monsterHealthBar.fillAmount = (float)inputMonsterHealth/monsterMaxHealth;
    }

    public void DisableMonsterHealthBar() {
        monsterHealthBar.gameObject.SetActive(false);
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