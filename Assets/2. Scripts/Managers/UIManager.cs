using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private GameObject popupWeaponSwitch;
    [ReadOnly, SerializeField] private Image hpUI;

    private void Awake()
    {
        popupWeaponSwitch = FindObjectOfType<WeaponSelectionPopup>().gameObject;
        popupWeaponSwitch.SetActive(false);
        
        hpUI = FindObjectOfType<PlayerHp>().GetComponent<Image>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            popupWeaponSwitch.SetActive(true);
        }

        if(Input.GetKeyUp(KeyCode.Tab))
        {
            popupWeaponSwitch.SetActive(false);
        }
    }

    public void UpdatePlayerHp(int playerHp)
    {
        hpUI.fillAmount = (float)playerHp/100f;
    }
}