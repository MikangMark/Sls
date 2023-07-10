using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterBuffType { POW = 0, WEAK, VULNER, IMPAIR, SLIMECARD, RESTRAINT, CONSCIOUS }
public class MonsterManager : MonoBehaviour
{
    //모든몬스터의 정보저장
    public MonsterExcelDataLoader monsterExcelData;
    public MonsterSkillExcelDataLoader skillData;

    public List<MonsterStat> spac;//모든몬스터의 스텟 및 스킬 정보 ex(슬라임,슬라임정보)

    public List<GameObject> monsterPfab;//모든몬스터 프리펩
    private void Start()
    {
        spac = new List<MonsterStat>();
        for(int i=0;i< monsterExcelData.monsterExelInfo.Count; i++)
        {
            spac.Add(monsterExcelData.monsterExelInfo[i]);
        }
        for (int i = 0; i < monsterExcelData.monsterExelInfo.Count; i++)
        {
            MonsterStat temp = new MonsterStat();
            temp = monsterExcelData.monsterExelInfo[i];
            for(int j = 0; j < monsterPfab[i].GetComponent<Monster>().stat.skillList.Count; j++)
            {
                //temp.skillList.Add(skillData.monsterSkillInfo[monsterPfab[i].GetComponent<Monster>().skillList[j]);
            }
            
            //monsterInfo.Add(monsterExcelData.monsterExelInfo[i].name, temp);
        }
    }

    public MonsterStat SearchMonsterStat(string name)
    {
        for(int i = 0; i < spac.Count; i++)
        {
            if (spac[i].name.Equals(name))
            {
                return spac[i];
            }
        }
        return null;
    }
}
