using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MonsterInfo
{
    public MonsterStat stat;
    public List<MonsterSkill> skill;

    public void InputInfo(MonsterStat _stat, List<MonsterSkill> _skill)
    {
        stat = _stat;
        skill = _skill;
    }
}
public class MonsterManager : MonoBehaviour
{
    //모든몬스터의 정보저장
    public MonsterExcelDataLoader monsterData;
    public MonsterSkillExcelDataLoader skillData;

    public List<MonsterInfo> monsterInfo;

    private void Start()
    {
        monsterInfo = new List<MonsterInfo>();
        for(int i = 0; i < monsterData.monsterExelInfo.Count; i++)
        {
            MonsterInfo temp = new MonsterInfo();
            temp.stat = monsterData.monsterExelInfo[i];
            temp.skill = skillData.monsterSkillInfo;
            monsterInfo.Add(temp);
        }
    }
}
