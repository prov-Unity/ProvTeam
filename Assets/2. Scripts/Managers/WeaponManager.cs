using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Fist, Bone, Ornate_Sword
}

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public List<GameObject> weaponPrefabs;
    private void Awake() {
        instance = this;

        weaponPrefabs = new List<GameObject>();
        weaponPrefabs.Add(null);
        weaponPrefabs.Add(null);
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Ornate_Sword"));
    }
}
