using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Fist_Left, Fist_Right, Bone_Right
}

public class WeaponManager : MonoBehaviour
{
    [ReadOnly] public static WeaponManager instance;
    [ReadOnly] public List<GameObject> weaponPrefabs;
    private void Awake() {
        instance = this;

        weaponPrefabs = new List<GameObject>();
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Fist"));
        weaponPrefabs.Add(weaponPrefabs[(int)WeaponType.Fist_Left]);
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Bone"));
    }
}