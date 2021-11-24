using System;
using System.Collections.Generic;
using UnityEngine;

public struct SaveData
{
    public string stageName;
    public DateTime saveDateTime;
    public Vector3 respawnPoint;
    public List<AvailableWeapon> savedAvailableWeapons;
    public int savedHealth;

    public SaveData(string inputStageName, DateTime inputSaveDateTime, Vector3 inputRespawnPoint, List<AvailableWeapon> inputSavedAvailableWeapon, int inputSavedHealth) {
        stageName = inputStageName;
        saveDateTime = inputSaveDateTime;
        respawnPoint = inputRespawnPoint;
        savedAvailableWeapons = inputSavedAvailableWeapon;
        savedHealth = inputSavedHealth;
    }
}
