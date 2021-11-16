using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public Weapon targetWeapon;
    public bool isNewWeapon;

    private Player player;
    private string targetName; 
    private RaycastHit hitInfo;

    private float distanceToInteract = 3f;

    private void Awake() {
        targetWeapon = null;
        isNewWeapon = true;

        player = GetComponent<Player>();
    }

    private void Update() {
        if(Physics.Raycast(player.neckTransform.position, (player.neckTransform.position - Camera.main.transform.position), out hitInfo, distanceToInteract, LayerMask.GetMask("Interactable"))) {
            if(hitInfo.collider.GetComponent<Weapon>().isSelected) {
                UIManager.instance.EnableInteractionPopup();
                
                targetName = hitInfo.collider.GetComponent<Weapon>().weaponType.ToString().Split(new char[] {'_'})[0];
                targetWeapon = hitInfo.collider.GetComponent<Weapon>();

                // check whether this weapon is the new weapon for the player
                // if(player.playerInfo.availableWeapons.Find(x => x.weaponType == hitInfo.collider.GetComponent<Weapon>().weaponType)) {
                //     ;
                // }

                // // this branch is for the new weapon
                // UIManager.instance.SetInteractionPopupText($"Press E to pickup {targetName}");
                // isNewWeapon = true;

                // // this branch is for the existing weapon
                // UIManager.instance.SetInteractionPopupText($"Press E to replace {targetName}");
                // isNewWeapon = false;
            }
            else {
                UIManager.instance.DisableInteractionPopup();
                targetWeapon = null;
            }
        }
        else {
            UIManager.instance.DisableInteractionPopup();
            targetWeapon = null;
        }
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.blue;

    //     Gizmos.DrawRay(player.neckTransform.position, (player.neckTransform.position - Camera.main.transform.position) * distanceToInteract);
    // }
}
