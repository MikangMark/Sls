using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public enum Intent { ATK = 0, DEF, BUF, DBUF, ATK_DEF, ATK_BUF, ATK_DBUF, DEF_BUF, DEF_DBUF, BUF_DBUF};
    public string monsterName;//인스펙터에서 입력되어있는 몬스터이름
    public MonsterStat stat;//해당몬스터의 스텟
    public List<MonsterSkill> skill;//해당몬스터가 보유중인 스킬
    public List<int> skillCord;//해당 몬스터가 보유하고있는 스킬 코드 인스팩터에 입력되어있음
    public MonsterManager monsterManager;
    public Dictionary<MonsterBuffType, int> bufList;//해당 몬스터가 보유하고있는 버프/디버프 리스트 key 버프타입 value 적용된 숫자
    public int shiled;
    public Intent intent;// 다음턴에행동할 의도\

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
        #region 의도 타입하드코딩
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

    //몬스터한마리마다 이 스크립트를 소유
    //몬스터 공격수치 체력 방어수치 사용하는 몬스터이미지 의도이미지(Intent) 의도타입 사용하는스킬코드
    //몬스터마다 미리 프리펩 생성되어있음
}
