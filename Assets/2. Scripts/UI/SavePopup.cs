using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private SaveSlots saveSlots;

    [ReadOnly, SerializeField] private string tempSlotName;
    [ReadOnly, SerializeField] private int slotIndex;

    private void Awake() {
        saveSlots = GetComponentInChildren<SaveSlots>();

        slotIndex = 0;
    }

    private void Start() {
        for(; slotIndex < SaveLoadManager.instance.maxSaveSlot; slotIndex++) {
            DisableCurrentSaveSlot();
        }

        slotIndex = 0;
        EnableCurrentSaveSlot();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) 
            MoveCurrentSaveSlotUp();
        else if(Input.GetKeyDown(KeyCode.DownArrow)) 
            MoveCurrentSaveSlotDown();
        else if(Input.GetKeyDown(KeyCode.Return)) {
            SaveToCurrentSlot();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.instance.SetTimeScale(1f);
            GameManager.instance.EnablePlayerInput();
            gameObject.SetActive(false);
        }
    }

    private void MoveCurrentSaveSlotUp() {
        DisableCurrentSaveSlot();
        slotIndex--;
        if(slotIndex < 0)
            slotIndex = SaveLoadManager.instance.maxSaveSlot-1;
        EnableCurrentSaveSlot();
    }
    private void MoveCurrentSaveSlotDown() {
        DisableCurrentSaveSlot();
        slotIndex++;
        if(slotIndex >= SaveLoadManager.instance.maxSaveSlot)
            slotIndex = 0;
        EnableCurrentSaveSlot();
    }

    private void DisableCurrentSaveSlot() {
        if(saveSlots.slots[slotIndex].IsSelected())
            saveSlots.slots[slotIndex].DisableSelected();
    }

    private void EnableCurrentSaveSlot() {
        if(saveSlots.slots[slotIndex].IsSelected() == false)
            saveSlots.slots[slotIndex].EnableSelected();
    }

    public void LoadFiles() {
        // do something -> this would be implemented right after understanding the usage of easy save
        // Debug.Log("Save data loading is done (not done actually)");
        // for(int curIndex = 0; curIndex < SaveLoadManager.instance.maxSaveSlot; curIndex++) {
        //     tempSlotName = (curIndex + 1) + ": ";
        //     // file existence check
        //     // currently it's not implemented
        //     tempSlotName += "empty";

        //     saveSlots.slots[curIndex].UpdateSaveName(tempSlotName);
        // }


        SaveLoadManager.instance.LoadData();
        for(int curIndex = 0; curIndex < SaveLoadManager.instance.maxSaveSlot; curIndex++) {
            saveSlots.slots[curIndex].UpdateSaveName(curIndex + ": " + SaveLoadManager.instance.data[curIndex].GetSaveSlotName());
        }
    }
    private void SaveToCurrentSlot() {
        // do something -> this would be implemented right after understanding the usage of easy save
        // Debug.Log("Save is done (not done actually)");

        SavePoint targetSavePoint =  GameManager.instance.player.playerInteraction.targetSavePoint;
        SaveLoadManager.instance.SaveData(slotIndex, new SaveData(targetSavePoint.stageName, DateTime.Now, targetSavePoint.respawnPoint, GameManager.instance.player.playerInfo.availableWeapons, GameManager.instance.player.playerInfo.health));
    }
}