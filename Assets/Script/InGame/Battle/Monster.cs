using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public enum Intent { ATK = 0, DEF, BUF, DBUF, ATK_DEF, ATK_BUF, ATK_DBUF, DEF_BUF, DEF_DBUF, BUF_DBUF};
    public string monsterName;//�ν����Ϳ��� �ԷµǾ��ִ� �����̸�
    public MonsterStat stat;//�ش������ ����
    public List<MonsterSkill> skill;//�ش���Ͱ� �������� ��ų
    public List<int> skillCord;//�ش� ���Ͱ� �����ϰ��ִ� ��ų �ڵ� �ν����Ϳ� �ԷµǾ�����
    public MonsterManager monsterManager;
    public Dictionary<MonsterBuffType, int> bufList;//�ش� ���Ͱ� �����ϰ��ִ� ����/����� ����Ʈ key ����Ÿ�� value ����� ����
    public int shiled;
    public Intent intent;// �����Ͽ��ൿ�� �ǵ�\

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
        #region �ǵ� Ÿ���ϵ��ڵ�
        if (nextSkill.type.Count > 1)
        {
            switch (nextSkill.type[0])
            {
                case SkillType.ATK:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.ATK_BUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.ATK_DEF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.ATK_BUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.ATK_DBUF;
                            break;
                    }
                    break;
                case SkillType.CONSCIOUS:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_BUF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.BUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF_BUF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.BUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.BUF_DBUF;
                            break;
                    }
                    break;
                case SkillType.DEF:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_DEF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.DEF_BUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.DEF_BUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.DEF_DBUF;
                            break;
                    }
                    break;
                case SkillType.IMPAIR:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.DBUF;
                            break;
                    }
                    break;
                case SkillType.POW:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_BUF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.BUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF_BUF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.BUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.BUF_DBUF;
                            break;
                    }
                    break;
                case SkillType.RESTRAINT:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.DBUF;
                            break;
                    }
                    break;
                case SkillType.SLIMECARD:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.DBUF;
                            break;
                    }
                    break;
                case SkillType.VULNER:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.DBUF;
                            break;
                    }
                    break;
                case SkillType.WEAK:
                    switch (nextSkill.type[1])
                    {
                        case SkillType.ATK:
                            intent = Intent.ATK_DBUF;
                            break;
                        case SkillType.CONSCIOUS:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.DEF:
                            intent = Intent.DEF_DBUF;
                            break;
                        case SkillType.IMPAIR:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.POW:
                            intent = Intent.BUF_DBUF;
                            break;
                        case SkillType.RESTRAINT:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.SLIMECARD:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.VULNER:
                            intent = Intent.DBUF;
                            break;
                        case SkillType.WEAK:
                            intent = Intent.DBUF;
                            break;
                    }
                    break;
            }
        }
        else
        {
            switch (nextSkill.type[0])
            {
                case SkillType.ATK:
                    intent = Intent.ATK;
                    break;
                case SkillType.CONSCIOUS:
                    intent = Intent.BUF;
                    break;
                case SkillType.DEF:
                    intent = Intent.DEF;
                    break;
                case SkillType.IMPAIR:
                    intent = Intent.DBUF;
                    break;
                case SkillType.POW:
                    intent = Intent.BUF;
                    break;
                case SkillType.RESTRAINT:
                    intent = Intent.DBUF;
                    break;
                case SkillType.SLIMECARD:
                    intent = Intent.DBUF;
                    break;
                case SkillType.VULNER:
                    intent = Intent.DBUF;
                    break;
                case SkillType.WEAK:
                    intent = Intent.DBUF;
                    break;
            }

        }
        #endregion
    }

    //�����Ѹ������� �� ��ũ��Ʈ�� ����
    //���� ���ݼ�ġ ü�� ����ġ ����ϴ� �����̹��� �ǵ��̹���(Intent) �ǵ�Ÿ�� ����ϴ½�ų�ڵ�
    //���͸��� �̸� ������ �����Ǿ�����
}
