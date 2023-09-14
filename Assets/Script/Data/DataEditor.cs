using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DataEditor : MonoBehaviour
{
    public CardData data;
    public ExcelDataLoader excelDataLoader;
    public Deck deck;
    public InGameUI inGameUI;
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
        data.dataLoader.cardInfo = data.items;
        
        data.dataLoader.InitSettingCardDatas();
        deck.InitDeck_Info();
        inGameUI.CreateDeckObj();
        //InGame.Instance.SetReward();
    }
}