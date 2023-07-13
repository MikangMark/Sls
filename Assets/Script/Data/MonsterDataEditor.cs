using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class MonsterDataEditor : MonoBehaviour
{
    public MonsterData data;
    public MonsterExcelDataLoader monsterExcelDataLoader;
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
        data.monsterDataLoader.monsterExelInfo = data.items;
        monsterExcelDataLoader.monsterExelInfo = data.items;
    }
}
