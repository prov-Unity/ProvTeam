using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public string stageName;
    [ReadOnly] public Vector3 respawnPoint;

    private void Awake() {
        respawnPoint = transform.position + new Vector3(0, 40f, 0);
    }
}
