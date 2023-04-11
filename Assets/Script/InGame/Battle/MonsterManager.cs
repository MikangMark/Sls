using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterBuffType { POW = 0, WEAK }
public class MonsterInfo
{
    public MonsterStat stat;//������ ����
    public List<MonsterSkill> skill;//����ϴ� ��ų ����Ʈ
    public List<int> skillCord;//����� ��ų �ڵ� �Է� �ν�����â���� �Է� ����

    public MonsterInfo()
    {
        stat = new MonsterStat();
        skill = new List<MonsterSkill>();
        skillCord = new List<int>();
    }
    
}
public class MonsterManager : MonoBehaviour
{
    //�������� ��������
    public MonsterExcelDataLoader monsterData;
    public MonsterSkillExcelDataLoader skillData;

    public Dictionary<string, MonsterInfo> monsterInfo;//�������� ���� �� ��ų ���� ex(������,����������)

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
