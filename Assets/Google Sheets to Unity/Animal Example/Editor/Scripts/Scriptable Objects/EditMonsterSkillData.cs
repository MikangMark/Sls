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
[CreateAssetMenu(fileName = "EditMonsterSkillData")]
public class EditMonsterSkillData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<MonsterSkill> items = new List<MonsterSkill>();
    [HideInInspector]
    public MonsterSkillExcelDataLoader monsterSkillDataLoader;
    public List<string> Names = new List<string>();
    public EditMonsterData editMonster;
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        MonsterSkill temp;
        temp = new MonsterSkill();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "INDEX":
                    temp.index = int.Parse(list[i].value);
                    break;
                case "NAME":
                    temp.name = list[i].value;
                    break;
                case "ATK":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.ATK);
                    temp.skillValue.Add(SkillType.ATK, int.Parse(list[i].value));
                    break;
                case "DEF":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.DEF);
                    temp.skillValue.Add(SkillType.DEF, int.Parse(list[i].value));
                    break;
                case "POW":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.POW);
                    temp.skillValue.Add(SkillType.POW, int.Parse(list[i].value));
                    break;
                case "WEAK":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.WEAK);
                    temp.skillValue.Add(SkillType.WEAK, int.Parse(list[i].value));
                    break;
                case "VULNER":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.VULNER);
                    temp.skillValue.Add(SkillType.VULNER, int.Parse(list[i].value));
                    break;
                case "IMPAIR":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.IMPAIR);
                    temp.skillValue.Add(SkillType.IMPAIR, int.Parse(list[i].value));
                    break;
                case "SLIMECARD":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.SLIMECARD);
                    temp.skillValue.Add(SkillType.SLIMECARD, int.Parse(list[i].value));
                    break;
                case "RESTRAINT":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.RESTRAINT);
                    temp.skillValue.Add(SkillType.RESTRAINT, int.Parse(list[i].value));
                    break;
                case "CONSCIOUS":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.CONSCIOUS);
                    temp.skillValue.Add(SkillType.CONSCIOUS, int.Parse(list[i].value));
                    break;
            }
        }
        for(int i = 0; i < editMonster.items.Count; i++)
        {
            if (editMonster.items[i].name.Equals(temp.name))
            {
                editMonster.items[i].skillList.Add(temp);
            }
        }
        items.Add(temp);
    }

}

[CustomEditor(typeof(EditMonsterSkillData))]
public class MonsterSkillDataEditor : Editor
{
    EditMonsterSkillData data;

    void OnEnable()
    {
        data = (EditMonsterSkillData)target;
        data.monsterSkillDataLoader = GameObject.Find("ExcelData").GetComponent<MonsterSkillExcelDataLoader>();
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
        data.monsterSkillDataLoader.monsterSkillInfo = data.items;
        EditorUtility.SetDirty(target);

    }

}