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
[CreateAssetMenu(fileName = "EditMonsterData")]
public class EditMonsterData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<MonsterStat> items = new List<MonsterStat>();
    [HideInInspector]
    public MonsterExcelDataLoader monsterDataLoader;
    public List<string> Names = new List<string>();

    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        MonsterStat temp;
        temp = new MonsterStat();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "NAME":
                    temp.name = list[i].value;
                    temp.img = Resources.Load<Sprite>("MonsterImg/" + list[i].value);
                    break;
                case "HP":
                    temp.maxHp = int.Parse(list[i].value);
                    temp.hp = int.Parse(list[i].value);
                    break;
                case "SHIELD":
                    temp.shield = int.Parse(list[i].value);
                    break;
            }
        }
        items.Add(temp);
    }

}

[CustomEditor(typeof(EditMonsterData))]
public class MonsterDataEditor : Editor
{
    EditMonsterData data;

    void OnEnable()
    {
        data = (EditMonsterData)target;
        data.monsterDataLoader = GameObject.Find("ExcelData").GetComponent<MonsterExcelDataLoader>();
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
        data.monsterDataLoader.monsterExelInfo = data.items;
        EditorUtility.SetDirty(target);

    }

}