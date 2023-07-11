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
public class MonsterData : MonoBehaviour
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<MonsterStat> items = new List<MonsterStat>();
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
                    break;
                case "HP":
                    temp.maxHp = int.Parse(list[i].value);
                    temp.hp = int.Parse(list[i].value);
                    break;
                case "SHIELD":
                    temp.shield = int.Parse(list[i].value);
                    break;
                case "IMG":
                    temp.img = Resources.Load<Sprite>("MonsterImg/" + list[i].value);
                    break;
            }
        }
        items.Add(temp);
    }

}