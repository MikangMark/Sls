using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class CardData : MonoBehaviour
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<CardInfo> items = new List<CardInfo>();
    [HideInInspector]
    public ExcelDataLoader dataLoader;
    public List<string> Names = new List<string>();

    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        CardInfo temp;
        temp = new CardInfo();
        for (int i = 1; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "INDEX":
                    temp.index = int.Parse(list[i].value);
                    break;
                case "COST":
                    temp.cost = int.Parse(list[i].value);
                    break;
                case "CARDTYPE":
                    System.Enum.TryParse(list[i].value, out temp.cardType);
                    break;
                case "TYPE":
                    System.Enum.TryParse(list[i].value, out temp.type);
                    break;
                case "TYPE_SUB":
                    System.Enum.TryParse(list[i].value, out temp.subType);
                    break;
                case "VALUE":
                    temp.skillValue.Add(temp.type, int.Parse(list[i].value));
                    break;
                case "VALUE_SUB":
                    temp.skillValue.Add(temp.subType, int.Parse(list[i].value));
                    break;
                case "NAME":
                    temp.title = list[i].value;
                    break;
                case "TEXT":
                    temp.text = list[i].value;
                    break;
            }
        }
        items.Add(temp);
    }

}
[System.Serializable]
public class DataEditor : MonoBehaviour
{
    public CardData data;
    void Start()
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
        data.dataLoader.cardInfo = data.items;

    }
}