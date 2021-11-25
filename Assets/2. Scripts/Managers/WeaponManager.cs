using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum WeaponType {
    Fist_Left, Fist_Right, Bone, Sword_02, Dagger_01, Dagger_02, Axe_01, Axe_02, Axe_03, Spear_01, Spear_02, TwoHandSword_01, TwoHandSword_02, BlackHole, IceWheel, Spider, GolemWeapon, No_Weapon
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
    [ReadOnly] public List<Sprite> weaponIcons;
    [ReadOnly] public List<int> weaponAttackPowers;
    [ReadOnly] public List<int> weaponInitialDurabilities;
    [ReadOnly] public List<int> maxComboIndexOfWeapons;

    private void Awake() {
        instance = this;

        weaponPrefabs = new List<GameObject>();
        weaponIcons = new List<Sprite>();
        weaponAttackPowers = new List<int>();
        weaponInitialDurabilities = new List<int>();
        maxComboIndexOfWeapons = new List<int>();


        // Fist_Left
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Fist"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Fist"));
        weaponAttackPowers.Add(5);
        weaponInitialDurabilities.Add(100);
        maxComboIndexOfWeapons.Add(3);
        // Fist_Right
        weaponPrefabs.Add(weaponPrefabs[(int)WeaponType.Fist_Left]);
        weaponIcons.Add(weaponIcons[(int)WeaponType.Fist_Left]);
        weaponAttackPowers.Add(5);
        weaponInitialDurabilities.Add(100);
        maxComboIndexOfWeapons.Add(3);

        // Bone
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Bone"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Bone"));
        weaponAttackPowers.Add(10);
        weaponInitialDurabilities.Add(7);
        maxComboIndexOfWeapons.Add(1);



        // the powers and durabilities of weapons below must be changed based on the table
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Sword_02"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Sword_02"));
        weaponAttackPowers.Add(20);
        weaponInitialDurabilities.Add(25);
        maxComboIndexOfWeapons.Add(1);



        // Dagger_01
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Dagger_01"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Dagger_01"));
        weaponAttackPowers.Add(15);
        weaponInitialDurabilities.Add(20);
        maxComboIndexOfWeapons.Add(1);

        // Dagger_02
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Dagger_02"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Dagger_02"));
        weaponAttackPowers.Add(15);
        weaponInitialDurabilities.Add(20);
        maxComboIndexOfWeapons.Add(1);



        // Axe_01
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Axe_01"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Axe_01"));
        weaponAttackPowers.Add(23);
        weaponInitialDurabilities.Add(24);
        maxComboIndexOfWeapons.Add(1);

        // Axe_02
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Axe_02"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Axe_02"));
        weaponAttackPowers.Add(23);
        weaponInitialDurabilities.Add(24);
        maxComboIndexOfWeapons.Add(1);

        // Axe_03
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Axe_03"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Axe_03"));
        weaponAttackPowers.Add(23);
        weaponInitialDurabilities.Add(24);
        maxComboIndexOfWeapons.Add(1);


        // Spear_01
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Spear_01"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Spear_01"));
        weaponAttackPowers.Add(27);
        weaponInitialDurabilities.Add(26);
        maxComboIndexOfWeapons.Add(1);

        // Spear_02
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/Spear_02"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/Spear_02"));
        weaponAttackPowers.Add(27);
        weaponInitialDurabilities.Add(26);
        maxComboIndexOfWeapons.Add(1);



        // TwoHandSword_01
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/TwoHandSword_01"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/TwoHandSword_01"));
        weaponAttackPowers.Add(30);
        weaponInitialDurabilities.Add(25);
        maxComboIndexOfWeapons.Add(5);

        // TwoHandSword_02
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/TwoHandSword_02"));
        weaponIcons.Add(Resources.Load<Sprite>("UIWeaponsIcon/TwoHandSword_02"));
        weaponAttackPowers.Add(30);
        weaponInitialDurabilities.Add(25);
        maxComboIndexOfWeapons.Add(5);



        // TwoHandedSword
        weaponPrefabs.Add(Resources.Load<GameObject>("Weapons/TwoHandedSword"));
        weaponAttackPowers.Add(12);
        weaponInitialDurabilities.Add(10);
    }
}