using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Fist_Left, Fist_Right, TwoHand_Sword_Right
}

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public List<GameObject> weaponPrefabs;
    private void Awake() {
        instance = this;

        weaponPrefabs = new List<GameObject>();
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Fist"));
        weaponPrefabs.Add(weaponPrefabs[(int)WeaponType.Fist_Left]);
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Ornate_Sword"));

    }
}
