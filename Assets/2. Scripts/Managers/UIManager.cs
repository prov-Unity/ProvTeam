using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [ReadOnly] public static UIManager instance;

    [ReadOnly, SerializeField] private Image playerHealthBar;
    [ReadOnly, SerializeField] private Image monsterHealthBar;

    [ReadOnly, SerializeField] private GameObject popupWeaponSwitch;

    private void Awake()
    {        
        instance = this;

        playerHealthBar = FindObjectOfType<PlayerHealthBar>().GetComponent<Image>();
        monsterHealthBar = FindObjectOfType<MonsterHealthBar>().GetComponent<Image>();
        monsterHealthBar.gameObject.SetActive(false);

        popupWeaponSwitch = FindObjectOfType<WeaponSelectionPopup>().gameObject;
        popupWeaponSwitch.SetActive(false);
    }

    private void Update()
    {
        ;
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

    public void EnableWeaponSwitchPopup() {
        popupWeaponSwitch.SetActive(true);
    }

    public void DisableWeaponSwitchPopup() {
        popupWeaponSwitch.SetActive(false);
    }

}