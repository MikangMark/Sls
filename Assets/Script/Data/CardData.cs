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
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    System.Enum.TryParse(list[i].value, out temp.subType);
                    break;
                case "VALUE":
                    temp.skillValue.Add(temp.type, int.Parse(list[i].value));
                    break;
                case "VALUE_SUB":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.skillValue.Add(temp.subType, int.Parse(list[i].value));
                    break;
                case "NAME":
                    temp.title = list[i].value;
                    temp.cardImg = Resources.Load<Sprite>("CardImg/" + temp.title);
                    break;
                case "TEXT":
                    temp.text = list[i].value;
                    break;
            }
        }
        items.Add(temp);
    }

}
