using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType { DEFAULT = 0, ATK, DEF, POW, WEAK, VULNER, IMPAIR, SLIMECARD, RESTRAINT, CONSCIOUS }
[System.Serializable]
public class MonsterSkill
{
    public int index;
    public string name;
    public List<SkillType> type;
    public Dictionary<SkillType, int> skillValue;
    public MonsterSkill()
    {
        type = new List<SkillType>();
        skillValue = new Dictionary<SkillType, int>();
    }
}

public class MonsterSkillExcelDataLoader : MonoBehaviour
{
    public List<MonsterSkill> monsterSkillInfo;
    public MonsterSkillData monsterSkillData;
    
    private void Awake()
    {
        monsterSkillInfo = monsterSkillData.items;
    }
    public void InitSetMonsterSkillDatas()
    {
        monsterSkillInfo = monsterSkillData.items;
    }

}
