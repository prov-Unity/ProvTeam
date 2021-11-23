using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [ReadOnly, SerializeField] private Vector3 spawnPoint;

    private void Awake() {
        spawnPoint = transform.position + new Vector3(0, 5f, 0);
    }
}
