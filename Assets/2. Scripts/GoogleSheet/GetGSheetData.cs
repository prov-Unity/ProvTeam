using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class GetGSheetData : MonoBehaviour
{
    [SerializeField] private string sheetId;
    [SerializeField] private string jsonPath;
    [SerializeField] private string sheetName;

    public List<List<object>> sheetRawData;
    private ReadSheet _readSheet;
    
    
    private void Awake()
    {
        _readSheet = new ReadSheet(sheetId, jsonPath);
        sheetRawData = new List<List<object>>();
    }

    [ContextMenu("GET DATA")]
    private void GetData()
    {
        IList<IList<object>> data = _readSheet.GetSheetRange(sheetName);

        foreach (var data0 in data)
        {
            sheetRawData.Add(data0.ToList());
        }
    }

    [ContextMenu("PRINT DATA")]
    private void Print()
    {
        for (int i = 0; i < sheetRawData.Count; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < sheetRawData[i].Count; j++)
                    Debug.Log($"Col : {j} $Header : {sheetRawData[i][j]}");
            }
            else
            {
                for (int j = 0; j < sheetRawData[i].Count; j++)
                    Debug.Log($"Col : {j} Contents : {sheetRawData[i][j]}");
            }
        }
    }
}