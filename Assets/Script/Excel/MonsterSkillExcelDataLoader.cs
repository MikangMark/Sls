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
    public string savedMonsterSKillKey = "SavedMonsterSkill";
    public string monsterskill;
    private void Awake()
    {
        monsterSkillInfo = monsterSkillData.items;
        if (monsterSkillInfo == null)
        {
            LoadSkillList();
        }
    }
    public void InitSetMonsterSkillDatas()
    {
        monsterSkillInfo = monsterSkillData.items;
        if(monsterSkillInfo != null)
        {
            SaveSkillList();
        }


    }
    public void SaveSkillList()
    {
        string json = JsonUtility.ToJson(new SerializableList<MonsterSkill> { items = monsterSkillInfo });
        PlayerPrefs.SetString(savedMonsterSKillKey, json);
        PlayerPrefs.Save();
    }

    public void LoadSkillList()
    {
        if (PlayerPrefs.HasKey(savedMonsterSKillKey))
        {
            string json = PlayerPrefs.GetString(savedMonsterSKillKey);
            SerializableList<MonsterSkill> serializableList = JsonUtility.FromJson<SerializableList<MonsterSkill>>(json);
            monsterSkillInfo = serializableList.items;
        }
    }

    [System.Serializable]
    private class SerializableList<T>
    {
        public List<T> items = new List<T>();
    }
}
