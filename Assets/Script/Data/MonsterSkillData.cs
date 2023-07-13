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
public class MonsterSkillData : MonoBehaviour
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<MonsterSkill> items = new List<MonsterSkill>();
    public MonsterSkillExcelDataLoader monsterSkillDataLoader;
    public List<string> Names = new List<string>();
    public MonsterData monsterData;

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
        for (int i = 0; i < monsterData.items.Count; i++)
        {
            if (monsterData.items[i].name.Equals(temp.name))
            {
                monsterData.items[i].skillList.Add(temp);
            }
        }
        items.Add(temp);
    }

}
