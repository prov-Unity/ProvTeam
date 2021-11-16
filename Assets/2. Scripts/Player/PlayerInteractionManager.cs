using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    private Player player;

    private float distanceToInteract = 3f;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        RaycastHit hit;
        if(Physics.Raycast(player.neckTransform.position, (player.neckTransform.position - Camera.main.transform.position), out hit, distanceToInteract, LayerMask.GetMask("Interactable"))) {
            Debug.Log(hit.collider.GetComponent<Weapon>().weaponType);
        }
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.blue;

    //     Gizmos.DrawRay(player.neckTransform.position, (player.neckTransform.position - Camera.main.transform.position) * distanceToInteract);
    // }
}
