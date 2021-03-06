using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveData
{
    public bool isLoaded;
    public string stageName;
    public DateTime saveDateTime;
    public Vector3 respawnPoint;
    public List<AvailableWeapon> savedAvailableWeapons;
    public int savedHealth;

    public SaveData(string inputStageName, DateTime inputSaveDateTime, Vector3 inputRespawnPoint, List<AvailableWeapon> inputSavedAvailableWeapon, int inputSavedHealth) {
        isLoaded = true;
        
        stageName = inputStageName;
        saveDateTime = inputSaveDateTime;
        respawnPoint = inputRespawnPoint;

        savedAvailableWeapons = inputSavedAvailableWeapon;
        savedHealth = inputSavedHealth;
    }

    public SaveData(bool trashValue) {
        isLoaded = false;

        stageName = "";
        saveDateTime = DateTime.MinValue;
        respawnPoint = Vector3.one;
        savedAvailableWeapons = null;
        savedHealth = -1;
    }

    public string GetSaveSlotName() {
        if(isLoaded) 
            return stageName + " " + saveDateTime.ToString("yyyy/mm/dd  hh/mm/ss");
        else 
            return "empty";
    }
}
