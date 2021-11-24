using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [ReadOnly] public static SaveLoadManager instance;

    [ReadOnly] public List<SaveData> data;
    [ReadOnly] public int maxSaveSlot;

    private string defaultSavePath;

    private void Awake() {
        instance = this;
        defaultSavePath = "SaveData/";

        maxSaveSlot = 5;

        InitialLoad();
    }

    private void InitialLoad() {
        for(int curIndex = 0; curIndex < maxSaveSlot; curIndex++) {
            if(Directory.Exists(defaultSavePath + curIndex + ".es3"))
                data.Add(ES3.Load<SaveData>(curIndex.ToString(), defaultSavePath + curIndex + ".es3"));
            else
                data.Add(new SaveData());
        }
    }

    public void LoadData() {
        if(Directory.Exists(defaultSavePath)) {
            for(int curIndex = 0; curIndex < maxSaveSlot; curIndex++) {
                if(Directory.Exists(defaultSavePath + curIndex + ".es3")) {
                    data[curIndex].SetSaveData(ES3.Load<SaveData>(curIndex.ToString(), defaultSavePath + curIndex + ".es3"));
                }
            }
        }
    }

    public void SaveData(int slotIndex, SaveData inputData) {
        ES3.Save(slotIndex.ToString(), inputData, defaultSavePath + slotIndex + ".es3");
    }
}
