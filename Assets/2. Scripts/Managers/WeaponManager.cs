using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Fist_Left, Fist_Right, Bone_Right, TwoHandedSword, No_Weapon
}

public class AvailableWeapon {
    public WeaponType weaponType;
    public int durability;

    public AvailableWeapon(WeaponType inputType, int inputDurability) {
        weaponType = inputType;
        durability = inputDurability;
    }
}

public class WeaponManager : MonoBehaviour
{
    [ReadOnly] public static WeaponManager instance;
    [ReadOnly] public List<GameObject> weaponPrefabs;
    [ReadOnly] public List<int> weaponAttackPowers;
    [ReadOnly] public List<int> weaponInitialDurabilities;
    private void Awake() {
        instance = this;

        weaponPrefabs = new List<GameObject>();
        weaponAttackPowers = new List<int>();
        weaponInitialDurabilities = new List<int>();

        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Fist"));
        weaponAttackPowers.Add(5);
        weaponInitialDurabilities.Add(100);
        weaponPrefabs.Add(weaponPrefabs[(int)WeaponType.Fist_Left]);
        weaponAttackPowers.Add(5);
        weaponInitialDurabilities.Add(100);

        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Bone"));
        weaponAttackPowers.Add(10);
        weaponInitialDurabilities.Add(7);
        
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/TwoHandedSword"));
        weaponAttackPowers.Add(12);
        weaponInitialDurabilities.Add(10);
    }
}