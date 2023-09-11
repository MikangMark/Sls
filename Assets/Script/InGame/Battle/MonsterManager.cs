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
    public InGameUI inGameUI;
    bool repairData = false;
    private void Start()
    {
        spac = monsterExcelData.monsterExelInfo;
    }
    private void FixedUpdate()
    {
        if (repairData)
        {
            return;
        }
        if (inGameUI.battle.thisActive)
        {
            try
            {
                if (spac[0].skillList[0].skillValue.Count == 0)
                {
                    spac = monsterExcelData.monsterExelInfo;
                    repairData = true;
                }
            }
            catch
            {
                spac = monsterExcelData.monsterExelInfo;
                repairData = true;
            }
            
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
