using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType {
    Weapon
}

public class PlayerInteractionManager : MonoBehaviour
{
    private Player player;
    private Weapon targetWeapon;

    private float distanceToInteract = 3f;

    private void Awake() {
        player = GetComponent<Player>();
        targetWeapon = null;
    }

    private void Update() {
        RaycastHit hit;
        if(Physics.Raycast(player.neckTransform.position, (player.neckTransform.position - Camera.main.transform.position), out hit, distanceToInteract, LayerMask.GetMask("Interactable"))) {
            if(hit.collider.GetComponent<Weapon>().isSelected) {
                UIManager.instance.EnableInteractionPopup();
                targetWeapon = hit.collider.GetComponent<Weapon>();
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
