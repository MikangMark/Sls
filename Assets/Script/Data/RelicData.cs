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
public class RelicData : MonoBehaviour
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<Relic> items = new List<Relic>();
    public RelicExcelDataLoader relicDataLoader;
    public List<string> Names = new List<string>();
    // Start is called before the first frame update
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        Relic temp;
        temp = new Relic();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "Index":
                    temp.index = int.Parse(list[i].value);
                    break;
                case "Title":
                    temp.title = list[i].value;
                    temp.img = Resources.Load<Sprite>("RelicImg/" + temp.title);
                    break;
                case "Target":
                    System.Enum.TryParse(list[i].value, out temp.target);
                    break;
                case "Tag":
                    System.Enum.TryParse(list[i].value, out temp.relicTag);
                    break;
                case "Value":
                    temp.value = float.Parse(list[i].value);
                    break;
                case "Text":
                    temp.text = list[i].value;
                    break;
                case "InfoText":
                    temp.infoText = list[i].value;
                    break;
                
            }
        }
        items.Add(temp);
    }
}
