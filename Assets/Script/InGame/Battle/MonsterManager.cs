using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterBuffType { POW = 0, WEAK, VULNER, IMPAIR, SLIMECARD, RESTRAINT, CONSCIOUS }
public class MonsterManager : MonoBehaviour
{
    //�������� ��������
    public MonsterExcelDataLoader monsterExcelData;
    public MonsterSkillExcelDataLoader skillData;

    public List<MonsterStat> spac;//�������� ���� �� ��ų ���� ex(������,����������)

    public List<GameObject> monsterPfab;//������ ������
    private void Start()
    {
        spac = monsterExcelData.monsterExelInfo;
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
