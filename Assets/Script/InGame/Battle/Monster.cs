using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    enum Intent { DEFAULT = -1, ATK = 0, DEF, BUF, DBUF, ATK_DEF, ATK_BUF, ATK_DBUF, DEF_BUF, DEF_DBUF, BUF_DBUF};
    public string monsterName;//인스펙터에서 입력되어있는 몬스터이름
    public MonsterStat stat;//해당몬스터의 스텟
    public List<MonsterSkill> skill;//해당몬스터가 보유중인 스킬
    public List<int> skillCord;//해당 몬스터가 보유하고있는 스킬 코드 인스팩터에 입력되어있음
    public MonsterManager monsterManager;
    public Dictionary<MonsterBuffType, int> bufList;//해당 몬스터가 보유하고있는 버프/디버프 리스트 key 버프타입 value 적용된 숫자
    public int shiled;
    [SerializeField]
    Intent intent;// 다음턴에행동할 의도

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


    //몬스터한마리마다 이 스크립트를 소유
    //몬스터 공격수치 체력 방어수치 사용하는 몬스터이미지 의도이미지(Intent) 의도타입 사용하는스킬코드
    //몬스터마다 미리 프리펩 생성되어있음
}
