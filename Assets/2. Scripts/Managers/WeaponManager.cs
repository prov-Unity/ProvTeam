using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum WeaponType {
    Fist_Left, Fist_Right, Bone, Dagger_01, Dagger_02, TwoHandSword_01, BlackHole, IceWheel, Spider, GolemWeapon, No_Weapon
}

[Serializable]
public class AvailableWeapon {
    public WeaponType weaponType;
    public int durability;

    public AvailableWeapon(WeaponType inputType, int inputDurability) {
        weaponType = inputType;
        durability = inputDurability;
    }

    public AvailableWeapon() {
        weaponType = WeaponType.No_Weapon;
        durability = -1;
    }
}

public class WeaponManager : MonoBehaviour
{
    [ReadOnly] public static WeaponManager instance;
    [ReadOnly] public List<GameObject> weaponPrefabs;
    [ReadOnly] public List<int> weaponAttackPowers;
    [ReadOnly] public List<int> weaponInitialDurabilities;
    [ReadOnly] public List<int> maxComboIndexOfWeapons;

    private void Awake() {
        instance = this;

        weaponPrefabs = new List<GameObject>();
        weaponAttackPowers = new List<int>();
        weaponInitialDurabilities = new List<int>();
        maxComboIndexOfWeapons = new List<int>();

        // Fist_Left
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Fist"));
        weaponAttackPowers.Add(5);
        weaponInitialDurabilities.Add(100);
        maxComboIndexOfWeapons.Add(3);
        // Fist_Right
        weaponPrefabs.Add(weaponPrefabs[(int)WeaponType.Fist_Left]);
        weaponAttackPowers.Add(5);
        weaponInitialDurabilities.Add(100);
        maxComboIndexOfWeapons.Add(3);
        // Bone_Right
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Bone"));
        weaponAttackPowers.Add(10);
        weaponInitialDurabilities.Add(7);
        maxComboIndexOfWeapons.Add(5);


        // TwoHandedSword
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/TwoHandedSword"));
        weaponAttackPowers.Add(12);
        weaponInitialDurabilities.Add(10);
    }
}