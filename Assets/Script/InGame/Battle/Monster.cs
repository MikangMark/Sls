using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    enum Intent { DEFAULT = -1, ATK = 0, DEF, BUF, DBUF, ATK_DEF, ATK_BUF, ATK_DBUF, DEF_BUF, DEF_DBUF, BUF_DBUF};
    public string monsterName;//�ν����Ϳ��� �ԷµǾ��ִ� �����̸�
    public MonsterStat stat;//�ش������ ����
    public List<MonsterSkill> skill;//�ش���Ͱ� �������� ��ų
    public List<int> skillCord;//�ش� ���Ͱ� �����ϰ��ִ� ��ų �ڵ� �ν����Ϳ� �ԷµǾ�����
    public MonsterManager monsterManager;
    public Dictionary<MonsterBuffType, int> bufList;//�ش� ���Ͱ� �����ϰ��ִ� ����/����� ����Ʈ key ����Ÿ�� value ����� ����
    public int shiled;
    [SerializeField]
    Intent intent;// �����Ͽ��ൿ�� �ǵ�

    private void Start()
    {
        shiled = 0;
        monsterManager = GameObject.Find("DataObj").GetComponent<MonsterManager>();
        stat = monsterManager.monsterInfo[monsterName].stat;
        for(int i = 0; i < skillCord.Count; i++)
        {
            skill.Add(monsterManager.monsterInfo[monsterName].skill[i]);
        }
        bufList = new Dictionary<MonsterBuffType, int>();
        bufList.Add(MonsterBuffType.POW, 0);
        bufList.Add(MonsterBuffType.WEAK, 0);
        intent = Intent.DEFAULT;
    }

    public void NextUseSkill(int cord)
    {

    }

    public int SelectRandSkill()
    {


        return 0;
    }


    //�����Ѹ������� �� ��ũ��Ʈ�� ����
    //���� ���ݼ�ġ ü�� ����ġ ����ϴ� �����̹��� �ǵ��̹���(Intent) �ǵ�Ÿ�� ����ϴ½�ų�ڵ�
    //���͸��� �̸� ������ �����Ǿ�����
}
