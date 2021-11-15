using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            UIManager.instance.EnableWeaponSwitchPopup();
            GameManager.instance.SetTimeScale(0.1f);
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            UIManager.instance.DisableWeaponSwitchPopup();
            GameManager.instance.SetTimeScale(1);
        }
    }
}
