using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public Weapon targetWeapon;
    public bool isNewWeapon;

    private Player player;
    private string targetName; 
    private Collider [] colliders;

    private float distanceToInteract = 0.6f;

    private void Awake() {
        targetWeapon = null;
        isNewWeapon = true;

        player = GetComponent<Player>();
    }

    private void Update() {
        colliders = Physics.OverlapBox(player.transform.position + new Vector3((player.neckTransform.position-Camera.main.transform.position).normalized.x, 0.5f, (player.neckTransform.position-Camera.main.transform.position).normalized.z), new Vector3(distanceToInteract, player.transform.lossyScale.y, distanceToInteract), Quaternion.identity, LayerMask.GetMask("Interactable"));
        if(colliders.Length > 0) {
            targetWeapon = colliders[0].GetComponent<Weapon>();

            if(targetWeapon != null && !targetWeapon.isSelected) {
                UIManager.instance.EnableInteractionPopup();
                
                targetName = targetWeapon.weaponType.ToString().Split(new char[] {'_'})[0];
                // check whether this weapon is the new weapon for the player
                if(player.playerInfo.availableWeapons.Find(x => x.weaponType == targetWeapon.weaponType) == null) {
                    // this branch is for the new weapon
                    UIManager.instance.SetInteractionPopupText($"Press E to pickup {targetName}");
                    isNewWeapon = true;
                    // some code to pick up new weapon
                }
                else {
                    // this branch is for the existing weapon
                    UIManager.instance.SetInteractionPopupText($"Press E to replace {targetName}");
                    isNewWeapon = false;
                    // some code to replace existing weapon
                }
            }
            else {
                if(!UIManager.instance.isInteractionPopupDisabled) {
                    UIManager.instance.DisableInteractionPopup();
                    targetWeapon = null;
                }
            }
        }
        else {
            if(!UIManager.instance.isInteractionPopupDisabled) {
                UIManager.instance.DisableInteractionPopup();
                targetWeapon = null;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireCube(player.transform.position + new Vector3((player.neckTransform.position-Camera.main.transform.position).normalized.x, 0.5f, (player.neckTransform.position-Camera.main.transform.position).normalized.z), new Vector3(distanceToInteract, player.transform.lossyScale.y, distanceToInteract));
    }    
}
