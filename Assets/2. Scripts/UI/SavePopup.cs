using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private SaveSlots saveSlots;

    [ReadOnly, SerializeField] private string tempSlotName;
    [ReadOnly, SerializeField] private int slotIndex;
    [ReadOnly, SerializeField] private int maxSlotCount;


    private void Awake() {
        saveSlots = GetComponentInChildren<SaveSlots>();

        slotIndex = 0;
        maxSlotCount = 5;
    }

    private void Start() {
        LoadSaveFiles();

        for(; slotIndex < maxSlotCount; slotIndex++) {
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
            SaveToCurrentSaveSlot();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
    }

    private void MoveCurrentSaveSlotUp() {
        DisableCurrentSaveSlot();
        slotIndex--;
        if(slotIndex < 0)
            slotIndex = maxSlotCount-1;
        EnableCurrentSaveSlot();
    }
    private void MoveCurrentSaveSlotDown() {
        DisableCurrentSaveSlot();
        slotIndex++;
        if(slotIndex >= maxSlotCount)
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

    private void LoadSaveFiles() {
        // do something -> this would be implemented right after understanding the usage of easy save
        Debug.Log("Save data loading is done (not done actually)");
        for(int curIndex = 0; curIndex < maxSlotCount; curIndex++) {
            tempSlotName = (curIndex + 1) + ": ";
            // file existence check
            // currently it's not implemented
            tempSlotName += "empty";

            saveSlots.slots[curIndex].UpdateSaveName(tempSlotName);
        }
    }
    private void SaveToCurrentSaveSlot() {
        // do something -> this would be implemented right after understanding the usage of easy save
        Debug.Log("Save is done (not done actually)");
    }
}
