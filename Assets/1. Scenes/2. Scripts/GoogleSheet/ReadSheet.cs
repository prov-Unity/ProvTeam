using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

public class ReadSheet
{
    private string spreadsheetId = "1azLP4oge496mfkhfxFvYaiMjSCgwzgawcjYRHUbnTDs";
    private string jsonPath = "/StreamingAssets/Credentials/murder-mystery-unity-a9b247296fe9.json";

    private SheetsService service;
    
    public ReadSheet(string spreadsheetId, string jsonPath)
    {
        this.spreadsheetId = spreadsheetId;
        this.jsonPath = jsonPath;
        string fullJsonPath = Application.dataPath + jsonPath;

        Stream jsonCreds = (Stream)File.Open(fullJsonPath, FileMode.Open);

        ServiceAccountCredential credential = ServiceAccountCredential.FromServiceAccountData(jsonCreds);

        service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
        });
    }

    public IList<IList<object>> GetSheetRange(string sheetNameAndRange)
    {
        SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, sheetNameAndRange);

        ValueRange response = request.Execute();
        IList<IList<object>> values = response.Values;
        if (values != null && values.Count > 0)
        {
            return values;
        }
        else
        {
            Debug.Log("No data found.");
            return null;
        }
    }
}
