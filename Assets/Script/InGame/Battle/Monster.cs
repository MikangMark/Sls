using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public string monsterName;//�ν����Ϳ��� �ԷµǾ��ִ� �����̸�
    [ReadOnly]
    public MonsterStat stat;//�ش������ ����
    [ReadOnly]
    public List<MonsterSkill> skill;//�ش���Ͱ� �������� ��ų
    [ReadOnly]
    public List<int> skillCord;//�ش� ���Ͱ� �����ϰ��ִ� ��ų �ڵ�
    [ReadOnly]
    public MonsterManager monsterManager;
    private void Start()
    {

        monsterManager = GameObject.Find("DataObj").GetComponent<MonsterManager>();
        stat = monsterManager.monsterInfo[monsterName].stat;
        for(int i = 0; i < skillCord.Count; i++)
        {
            skill.Add(monsterManager.monsterInfo[monsterName].skill[i]);
        }
    }


    //�����Ѹ������� �� ��ũ��Ʈ�� ����
    //���� ���ݼ�ġ ü�� ����ġ ����ϴ� �����̹��� �ǵ��̹���(Intent) �ǵ�Ÿ�� ����ϴ½�ų�ڵ�
    //���͸��� �̸� ������ �����Ǿ�����
}
