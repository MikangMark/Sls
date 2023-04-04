using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public string monsterName;//인스펙터에서 입력되어있는 몬스터이름
    [ReadOnly]
    public MonsterStat stat;//해당몬스터의 스텟
    [ReadOnly]
    public List<MonsterSkill> skill;//해당몬스터가 보유중인 스킬
    [ReadOnly]
    public List<int> skillCord;//해당 몬스터가 보유하고있는 스킬 코드
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


    //몬스터한마리마다 이 스크립트를 소유
    //몬스터 공격수치 체력 방어수치 사용하는 몬스터이미지 의도이미지(Intent) 의도타입 사용하는스킬코드
    //몬스터마다 미리 프리펩 생성되어있음
}
