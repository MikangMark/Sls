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
public class EditCardData : ScriptableObject
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
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "INDEX":
                    temp.index = int.Parse(list[i].value);
                    break;
                case "COUNT":
                    temp.count = int.Parse(list[i].value);
                    break;
                case "RAND":
                    if(list[i].value == "TRUE")
                    {
                        temp.randomTarget = true;
                    }
                    else
                    {
                        temp.randomTarget = false;
                    }
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
                    if (list[i].value.Length > 0)
                    {
                        System.Enum.TryParse(list[i].value, out temp.subType);
                    }
                    break;
                case "VALUE":
                    temp.skillValue.Add(temp.type, int.Parse(list[i].value));
                    break;
                case "VALUE_SUB":
                    if (list[i].value.Length > 0)
                    {
                        temp.skillValue.Add(temp.subType, int.Parse(list[i].value));
                    }
                    break;
                case "NAME":
                    temp.title = list[i].value;
                    temp.cardImg = Resources.Load<Sprite>("CardImg/" + temp.title);
                    break;
                case "TEXT":
                    temp.text = list[i].value;
                    break;
                case "SHOP":
                    temp.shop = int.Parse(list[i].value);
                    break;
            }
        }
        items.Add(temp);
    }

}

[CustomEditor(typeof(EditCardData))]
public class DataEditor : Editor
{
    EditCardData data;
    public const string savedCardKey = "SavedCardKey";
    void OnEnable()
    {
        data = (EditCardData)target;
        data.dataLoader = GameObject.Find("ExcelData").GetComponent<ExcelDataLoader>();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");

        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
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
        EditorUtility.SetDirty(target);
        SaveCardList();
    }
    public void SaveCardList()
    {
        Debug.Log("카드정보저장");
        string json = JsonUtility.ToJson(new SerializableList<CardInfo> { items = data.items });
        PlayerPrefs.SetString(savedCardKey, json);
        PlayerPrefs.Save();
        LoadCardList();
    }
    
    public void LoadCardList()
    {
        if (PlayerPrefs.HasKey(savedCardKey))
        {
            string json = PlayerPrefs.GetString(savedCardKey);
            SerializableList<CardInfo> serializableList = JsonUtility.FromJson<SerializableList<CardInfo>>(json);
            data.items = serializableList.items;
            for(int i = 0; i < data.items.Count; i++)
            {
                Debug.Log(data.items[i].title);
                Debug.Log(data.items[i].type.ToString());
                Debug.Log(data.items[i].skillValue[data.items[i].type]);
            }
        }
    }

    public void TestLoadCardInfo()
    {
        if (PlayerPrefs.HasKey(savedCardKey))
        {
            string json = PlayerPrefs.GetString(savedCardKey);
            SerializableList<CardInfo> serializableList = JsonUtility.FromJson<SerializableList<CardInfo>>(json);
            data.items = serializableList.items;
            for (int i = 0; i < data.items.Count; i++)
            {
                Debug.Log(data.items[i].title);
                
            }
        }
    }

    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> items = new List<T>();
    }
}