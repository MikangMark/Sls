using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RelicDataEditor : MonoBehaviour
{
    public RelicData data;
    public RelicExcelDataLoader relicExcelDataLoader;
    void Awake()
    {
        UpdateStats(UpdateMethodOne);
    }
    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        data.items.Clear();
        foreach (string dataName in data.Names)
        {
            data.UpdateStats(ss.rows[dataName], dataName);
        }
        data.relicDataLoader.relicInfo = data.items;
        relicExcelDataLoader.relicInfo = data.items;
        data.relicDataLoader.InitSettingRelicDatas();
    }
}