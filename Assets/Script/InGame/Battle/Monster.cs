using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public string monsterName;//�ν����Ϳ��� �ԷµǾ��ִ� �����̸�
    public MonsterStat stat;//�ش������ ����
    public List<MonsterSkill> skill;//�ش���Ͱ� �������� ��ų
    public List<int> skillCord;//�ش� ���Ͱ� �����ϰ��ִ� ��ų �ڵ� �ν����Ϳ� �ԷµǾ�����
    public MonsterManager monsterManager;
    public Dictionary<MonsterBuffType, int> bufList;//�ش� ���Ͱ� �����ϰ��ִ� ����/����� ����Ʈ key ����Ÿ�� value ����� ����
    private void Start()
    {
        monsterManager = GameObject.Find("DataObj").GetComponent<MonsterManager>();
        stat = monsterManager.monsterInfo[monsterName].stat;
        for(int i = 0; i < skillCord.Count; i++)
        {
            skill.Add(monsterManager.monsterInfo[monsterName].skill[i]);
        }
        bufList = new Dictionary<MonsterBuffType, int>();
        bufList.Add(MonsterBuffType.POW, 0);
        bufList.Add(MonsterBuffType.WEAK, 0);
    }

    public void UseSkill(int cord)
    {

    }


    //�����Ѹ������� �� ��ũ��Ʈ�� ����
    //���� ���ݼ�ġ ü�� ����ġ ����ϴ� �����̹��� �ǵ��̹���(Intent) �ǵ�Ÿ�� ����ϴ½�ų�ڵ�
    //���͸��� �̸� ������ �����Ǿ�����
}
