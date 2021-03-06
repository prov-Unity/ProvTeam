using System;
using UnityEngine;

public class LoadPopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private SaveSlots saveSlots;

    [ReadOnly, SerializeField] private string tempSlotName;
    [ReadOnly, SerializeField] private int slotIndex;

    private void Awake() {
        saveSlots = GetComponentInChildren<SaveSlots>();
    }

    private void Start() {
        for(slotIndex = 0; slotIndex < SaveLoadManager.instance.maxSaveSlot; slotIndex++) {
            DisableCurrentSaveSlot();
        }
        slotIndex = 0;

        EnableCurrentSaveSlot();
        LoadFiles();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) 
            MoveCurrentSaveSlotUp();
        else if(Input.GetKeyDown(KeyCode.DownArrow)) 
            MoveCurrentSaveSlotDown();
        else if(Input.GetKeyDown(KeyCode.Return)) {
            LoadCurrentSlot();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
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
        for(int curIndex = 0; curIndex < SaveLoadManager.instance.maxSaveSlot; curIndex++) {
            saveSlots.slots[curIndex].UpdateSaveName(curIndex + ": " + SaveLoadManager.instance.data[curIndex].GetSaveSlotName());
        }
    }
    private void LoadCurrentSlot() {
        SaveData currentData = SaveLoadManager.instance.data[slotIndex];
        if(currentData.isLoaded) {
            MySceneManager.instance.loadedData = currentData;
            MySceneManager.instance.isInitial = false;
            MySceneManager.instance.LoadScene(currentData.stageName);
        }
    }
}