using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MonsterStat
{
    public string name;
    public int maxHp;
    public int hp;
    public Sprite img;
    public int shield;
    public List<MonsterSkill> skillList;

    public MonsterStat()
    {
        name = "";
        maxHp = 0;
        hp = 0;
        img = null;
        shield = 0;
        skillList = new List<MonsterSkill>();
    }

    public void InputInfo(string _name, int _hp, Sprite _img)
    {
        name = _name;
        hp = _hp;
        maxHp = _hp;
        img = _img;
        shield = 0;
    }

    public void AddSkill(MonsterSkill monsterSkill)
    {
        skillList.Add(monsterSkill);
    }
}
public class MonsterExcelDataLoader : MonoBehaviour
{
    public List<MonsterStat> monsterExelInfo;
    public MonsterData monsterData;

    public string savedMonsterStatKey = "SavedMonsterStat";
    private void Awake()
    {
        if (monsterExelInfo[0].skillList.Count == 0)
        {
            monsterExelInfo = monsterData.items;
        }
        
        SaveMonsterList();
    }
    public void InitSettingMonsterDatas()
    {
        if (monsterExelInfo[0].skillList.Count == 0)
        {
            monsterExelInfo = monsterData.items;
        }
        if (monsterExelInfo == null)
        {
            LoadMonseterList();
        }
    }
    // Start is called before the first frame update
    public void SaveMonsterList()
    {
        string json = JsonUtility.ToJson(new SerializableList<MonsterStat> { items = monsterExelInfo });
        PlayerPrefs.SetString(savedMonsterStatKey, json);
        PlayerPrefs.Save();
    }

    public void LoadMonseterList()
    {
        if (PlayerPrefs.HasKey(savedMonsterStatKey))
        {
            string json = PlayerPrefs.GetString(savedMonsterStatKey);
            SerializableList<MonsterStat> serializableList = JsonUtility.FromJson<SerializableList<MonsterStat>>(json);
            monsterExelInfo = serializableList.items;
        }
    }

    [System.Serializable]
    private class SerializableList<T>
    {
        public List<T> items = new List<T>();
    }
}
