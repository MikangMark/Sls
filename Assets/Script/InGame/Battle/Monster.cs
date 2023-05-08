using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    enum Intent { ATK = 0, DEF, BUF, DBUF, ATK_DEF, ATK_BUF, ATK_DBUF, DEF_BUF, DEF_DBUF, BUF_DBUF};
    public string monsterName;//�ν����Ϳ��� �ԷµǾ��ִ� �����̸�
    public MonsterStat stat;//�ش������ ����
    public List<MonsterSkill> skill;//�ش���Ͱ� �������� ��ų
    public List<int> skillCord;//�ش� ���Ͱ� �����ϰ��ִ� ��ų �ڵ� �ν����Ϳ� �ԷµǾ�����
    public MonsterManager monsterManager;
    public Dictionary<MonsterBuffType, int> bufList;//�ش� ���Ͱ� �����ϰ��ִ� ����/����� ����Ʈ key ����Ÿ�� value ����� ����
    public int weak;
    public int shiled;
    [SerializeField]
    Intent intent;// �����Ͽ��ൿ�� �ǵ�\

    public MonsterSkill nextSkill;

    private void Start()
    {
        shiled = 0;
        name = monsterName;
        monsterManager = GameObject.Find("DataObj").GetComponent<MonsterManager>();
        stat = monsterManager.monsterInfo[monsterName].stat;
        for(int i = 0; i < skillCord.Count; i++)
        {
            skill.Add(monsterManager.monsterInfo[monsterName].skill[i]);
        }
        GetComponent<Image>().sprite = stat.img;
        bufList = new Dictionary<MonsterBuffType, int>();
        bufList.Add(MonsterBuffType.POW, 0);
        bufList.Add(MonsterBuffType.WEAK, 0);
        NextUseSkill();
    }

    public void NextUseSkill()
    {
        nextSkill = skill[CreateSeed.Instance.RandNum(0, skill.Count)];
    }

    private void FixedUpdate()
    {
        weak = bufList[MonsterBuffType.WEAK];
    }

    //�����Ѹ������� �� ��ũ��Ʈ�� ����
    //���� ���ݼ�ġ ü�� ����ġ ����ϴ� �����̹��� �ǵ��̹���(Intent) �ǵ�Ÿ�� ����ϴ½�ų�ڵ�
    //���͸��� �̸� ������ �����Ǿ�����
}
