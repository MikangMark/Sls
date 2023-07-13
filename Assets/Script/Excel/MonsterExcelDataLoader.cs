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
    private void Awake()
    {
        monsterExelInfo = monsterData.items;
    }
    // Start is called before the first frame update
    
}
