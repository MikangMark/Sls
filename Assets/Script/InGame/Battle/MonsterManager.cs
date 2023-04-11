using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterBuffType { POW = 0, WEAK }
public class MonsterInfo
{
    public MonsterStat stat;//몬스터의 스텟
    public List<MonsterSkill> skill;//사용하는 스킬 리스트
    public List<int> skillCord;//사용할 스킬 코드 입력 인스펙터창에서 입력 받음

    public MonsterInfo()
    {
        stat = new MonsterStat();
        skill = new List<MonsterSkill>();
        skillCord = new List<int>();
    }
    
}
public class MonsterManager : MonoBehaviour
{
    //모든몬스터의 정보저장
    public MonsterExcelDataLoader monsterData;
    public MonsterSkillExcelDataLoader skillData;

    public Dictionary<string, MonsterInfo> monsterInfo;//모든몬스터의 스텟 및 스킬 정보 ex(슬라임,슬라임정보)

    private void Start()
    {
        monsterInfo = new Dictionary<string, MonsterInfo>();
        for(int i = 0; i < monsterData.monsterExelInfo.Count; i++)
        {
            MonsterInfo temp = new MonsterInfo();
            temp.stat = monsterData.monsterExelInfo[i];
            for(int j = 0; j < skillData.monsterSkillInfo.Count; j++)
            {
                temp.skill.Add(skillData.monsterSkillInfo[10001 + j]);
            }
            
            monsterInfo.Add(monsterData.monsterExelInfo[i].name, temp);
        }
    }
}
