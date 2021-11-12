using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private GameObject popupWeaponSwitch;
    // Start is called before the first frame update
    void Awake()
    {
        popupWeaponSwitch = GetComponentInChildren<Image>().gameObject;
        popupWeaponSwitch.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
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
}