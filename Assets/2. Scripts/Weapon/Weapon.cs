using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [ReadOnly] public float attackPower;

    private MeshCollider curCollider;

    private void Awake() {
        attackPower = 20f;

        curCollider = GetComponent<MeshCollider>();
    }

    public void EnableCollider() {
        curCollider.enabled = true;
    }

    public void DisableCollider() {
        curCollider.enabled = false;
    }
}