using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerBuffType { POW = 0, WEAK, VULNER, IMPAIR, SLIMECARD, RESTRAINT, CONSCIOUS }
public class Battle : MonoBehaviour
{
    public enum BattleResult { Win = 0, Lose }
    int divideCard;
    public int maxEnergy;
    public int energy;
    int refillEnergy;
    public int shiled;
    public Character.CharInfo stat;//전투중인 나의 스텟
    public Dictionary<PlayerBuffType, int> playerBufList;//플레이어 버프 리스트
    public List<GameObject> monsters;//전투중인 적의 리스트
    //public List<Monster> monstersList;
    public List<GameObject> battleDeck;//전투에서 사용할 나의 덱
    List<GameObject> abnomalDeck;//상태이상 카드 덱

    public List<GameObject> beforUse;//뽑을 카드모음
    public List<GameObject> afterUse;//사용한 카드모음
    public List<GameObject> deletCard;//소멸된 카드모음
    public List<GameObject> myHand;//나의 손패카드모음
    public GameObject myCardParent;

    public MonsterManager monsterManager;

    [SerializeField]
    GameObject playerPrf;
    [SerializeField]
    GameObject playerPos;

    public List<string> monsterGrup;
    public List<string[]> oneMonster;
    [SerializeField]
    GameObject monsterPos;

    [SerializeField]
    GameObject afterContent;
    [SerializeField]
    GameObject beforContent;
    [SerializeField]
    GameObject deleteContent;

    [SerializeField]
    GameObject cardPrf;
    public bool thisActive = false;

    public bool clear = false;
    public BattleResult result;

    void OnEnable()//setactive true될때 실행
    {
        thisActive = true;
        //전투시작 셋팅
        //1.덱불러오기 -> 에너지셋팅 -> 캐릭스텟 불러오기
        initData();
    }
    private void OnDisable()
    {
        InGame.Instance.currentFloor++;
        ClearData();
        thisActive = false;
    }
    private void Update()
    {
        DieMonster();
        
    }
    private void FixedUpdate()
    {
        InGame.Instance.charInfo.hp = stat.hp;
        if (EndBattle())
        {
            
            thisActive = false;
        }

    }
    public void OnClickEditorHealBtn()
    {
        stat.hp = InGame.Instance.charInfo.maxHp;
        Debug.Log("넌 죽지않아!");
    }
    public void OnClickEditorKillMonsterBtn()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            monsters[i].GetComponent<Monster>().stat.hp = 0;
        }
        Debug.Log("전원사망");
    }
    public void SetEnable(bool p_flag)
    {
        if( p_flag)
        {
            initData();
        }
    }

    void initData()
    {
        for(int i=0;i< Deck.Instance.cardList_Obj.Count; i++)
        {
            if(Deck.Instance.cardList_Obj[i] == null)
            {
                Deck.Instance.cardList_Obj.RemoveAt(i);
            }
        }
        abnomalDeck = new List<GameObject>();
        stat = new Character.CharInfo();
        Instantiate(playerPrf, playerPos.transform);
        battleDeck = new List<GameObject>(Deck.Instance.cardList_Obj);
        monsters = new List<GameObject>();
        //monstersList = new List<Monster>();
        oneMonster = new List<string[]>();
        beforUse = new List<GameObject>();
        
        for (int i = 0; i < monsterGrup.Count; i++)
        {
            oneMonster.Add(monsterGrup[i].Split(','));
        }

        CreateEnemy();
        for(int i = 0; i < battleDeck.Count; i++)
        {
            var copyobj = Instantiate(battleDeck[i], beforContent.transform);
            copyobj.GetComponent<OneCard>().thisCard.skillValue = battleDeck[i].GetComponent<OneCard>().thisCard.skillValue;
            beforUse.Add(copyobj);
            
        }
        myHand = new List<GameObject>();
        afterUse = new List<GameObject>();
        deletCard = new List<GameObject>();
        playerBufList = new Dictionary<PlayerBuffType, int>();
        for(PlayerBuffType i = PlayerBuffType.POW; i <= PlayerBuffType.CONSCIOUS; i++)
        {
            playerBufList.Add(i, 0);
        }
        stat = InGame.Instance.charInfo;
        shiled = 0;
        divideCard = 5;
        maxEnergy = 3;
        energy = maxEnergy;
        refillEnergy = maxEnergy;
        MyTurn();
    }
    void ClearData()
    {
        abnomalDeck = null;
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].GetComponent<Monster>().stat.hp = monsters[i].GetComponent<Monster>().stat.maxHp;
        }
        for (int i = monsters.Count; i > 0; i--)
        {
            Destroy(monsters[i-1]);
        }
        if (playerPos.transform.GetChild(0).gameObject != null)
        {
            Destroy(playerPos.transform.GetChild(0).gameObject);
        }
        battleDeck = null;
        monsters = null;
        //monstersList = null;
        oneMonster = null;
        for (int i = 0; i < beforContent.transform.childCount; i++)
        {
            Destroy(beforContent.transform.GetChild(i).gameObject);
        }
        beforUse = null;
        for(int i = 0; i < afterContent.transform.childCount; i++)
        {
            Destroy(afterContent.transform.GetChild(i).gameObject);
        }
        afterUse = null;
        for (int i = 0; i < myCardParent.transform.childCount; i++)
        {
            Destroy(myCardParent.transform.GetChild(i).gameObject);
        }
        myHand = null;
        for (int i = 0; i < deleteContent.transform.childCount; i++)
        {
            Destroy(deleteContent.transform.GetChild(i).gameObject);
        }
        deletCard = null;
        playerBufList = null;
    }
    void CreateEnemy()
    {
        int temp = -1;
        int randnum;
        for (int i = 0; i < 2; i++)
        {
            randnum = CreateSeed.Instance.RandNum(0, monsterGrup.Count);
            if (temp != randnum)
            {
                temp = randnum;
                GameObject oneMonster = Instantiate(monsterManager.monsterPfab[temp], monsterPos.transform);
                
                monsters.Add(oneMonster);
            }
            else
            {
                i--;
            }
            
        }
    }
    void MyTurn()
    {
        ShuffleDeck(beforUse);
        for (int i = 0; i < divideCard; i++)
        {
            CardDraw();
        }
        ReChargeEnergy(refillEnergy);
    }
    public void EndMyTurn()//턴종료눌렀을떄
    {
        //턴종료시 플레이어디버프 카운트 감소
        //쉴드 제거
        //가지고있는 패 전부다 버리기
        int del = myHand.Count;
        for (int i = 0; i < del; i++)
        {
            UsedCardMove(myHand[0]);
        }
        EnemyTurn();
    }
    public void EnemyTurn()
    {
        //의도대로 해당 애니매이션 실행뒤 다음 함수이동
        
        for (int i=0;i< monsters.Count; i++)
        {
            monsterManager.monsterExcelData.CompairMonsterSkillValue(monsters[i].GetComponent<Monster>());
            monsters[i].GetComponent<Monster>().PlaySkill();
            monsters[i].GetComponent<Monster>().NextUseSkill();
        }
        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        #region 몬스터버프감소
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].GetComponent<Monster>().stat.shield = 0;
            if (monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.IMPAIR] > 0)
            {
                monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.IMPAIR] -= 1;
            }
            if (monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.RESTRAINT] > 0)
            {
                monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.RESTRAINT] -= 1;
            }
            if (monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.VULNER] > 0)
            {
                monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.VULNER] -= 1;
            }
            if (monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.WEAK] > 0)
            {
                monsters[i].GetComponent<Monster>().bufList[MonsterBuffType.WEAK] -= 1;
            }

        }
        #endregion
        #region 플레이어버프 감소
        shiled = 0;
        if (playerBufList[PlayerBuffType.IMPAIR] > 0)
        {
            playerBufList[PlayerBuffType.IMPAIR] -= 1;
        }
        if (playerBufList[PlayerBuffType.RESTRAINT] > 0)
        {
            playerBufList[PlayerBuffType.RESTRAINT] -= 1;
        }
        if (playerBufList[PlayerBuffType.VULNER] > 0)
        {
            playerBufList[PlayerBuffType.VULNER] -= 1;
        }
        if (playerBufList[PlayerBuffType.WEAK] > 0)
        {
            playerBufList[PlayerBuffType.WEAK] -= 1;
        }
        #endregion
        //쉴드 제거, 디버프 카운트감소
        MyTurn();
    }
    public void ShuffleDeck<T>(List<T> list)//카드 셔플
    {
        // 리스트의 요소 수가 2 이상일 때만 셔플 진행
        if (list.Count > 1)
        {
            int n = list.Count;
            // 리스트를 랜덤하게 섞기 위해 Fisher-Yates 알고리즘을 사용
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    void CardDraw()//카드를 나눠줄때마다 실행
    {
        if (beforUse.Count <= 0)
        {
            beforUse.Clear();
            for(int i = 0; i < afterUse.Count; i++)
            {
                afterUse[i].transform.parent = beforContent.transform;
                beforUse.Add(afterUse[i]);
            }
            afterUse.Clear();
        }
        GameObject drawCard = beforUse[0];
        drawCard.transform.parent = myCardParent.transform;
        myHand.Add(drawCard);
        beforUse.RemoveAt(0);
    }
    public void UsedCardMove(GameObject target)
    {
        //GameObject temp = CreateAfterCardObj(target);
        target.transform.parent = afterContent.transform;
        afterUse.Add(target);
        for (int i = 0; i < myHand.Count; i++)
        {
            if(myHand[i] == target)
            {
                myHand.RemoveAt(i);
                break;
            }
        }
    }
    public void DeleteCardMove(GameObject target)
    {
        //GameObject temp = CreateDeleteCardObj(target);
        target.transform.parent = deleteContent.transform;
        deletCard.Add(target);
        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i] == target)
            {
                myHand.RemoveAt(i);
                break;
            }
        }
    }
    void ReChargeEnergy(int reEnergy)
    {
        energy = reEnergy;
    }
    #region 버프스크립트
    public void Attack(GameObject target, int value)
    {
        int damage = value;
        if (playerBufList[PlayerBuffType.POW] > 0)//플레이어가 힘버프를 받고있는가
        {
            damage += playerBufList[PlayerBuffType.POW];
        }
        if (target.GetComponent<Monster>().bufList[MonsterBuffType.WEAK] > 0)//타겟의 몬스터가 취약 디버프를 받고있는가
        {
            damage = damage + (int)(damage * 0.5);
        }
        if (target.GetComponent<Monster>().shiled > 0)//타겟의 몬스터가 쉴드를 가지고 있는가
        {
            if (target.GetComponent<Monster>().shiled >= damage)
            {
                target.GetComponent<Monster>().shiled -= damage;
            }
            else
            {
                damage -= target.GetComponent<Monster>().shiled;
                target.GetComponent<Monster>().shiled = 0;
            }
        }
        target.GetComponent<Monster>().stat.hp -= damage;
    }
    public void Deffence(GameObject target,int value)
    {
        if (playerBufList[PlayerBuffType.IMPAIR] > 0)
        {
            value = value - (int)(value / 3);
        }
        shiled += value;
    }
    public void Vulner(GameObject target, int value)
    {
        playerBufList[PlayerBuffType.VULNER] += value;
    }
    public void Power(GameObject target, int value)
    {
        
        switch (target.tag)
        {
            case "Monster":
                target.GetComponent<Monster>().bufList[MonsterBuffType.POW]++;
                break;

            case "Player":
                playerBufList[PlayerBuffType.POW] += value;
                break;
        }
        
    }
    public void Weak(GameObject target, int value)
    {
        target.GetComponent<Monster>().bufList[MonsterBuffType.WEAK] += value;
    }
    #endregion
    public void MonsterAtk(Monster monsterObj, int value)
    {
        if(monsterObj.bufList[MonsterBuffType.POW] > 0)
        {
            value += monsterObj.bufList[MonsterBuffType.POW];
        }
        if (playerBufList[PlayerBuffType.WEAK] > 0)
        {
            value = value + (int)(value * 0.5);
        }
        if (shiled > value)
        {
            shiled -= value;
        }
        else
        {
            int temp = value - shiled;
            stat.hp -= temp;
        }
    }
    public void MonsterDef(Monster monsterObj, int value)
    {
        monsterObj.stat.shield += value;
    }
    public void MonsterPow(Monster monsterObj, int value)
    {
        monsterObj.bufList[MonsterBuffType.POW] += value;
    }
    public void CreateSlimeCardObj()
    {
        GameObject temp = new GameObject();
        temp = Instantiate(cardPrf, beforContent.transform);
        temp.GetComponent<OneCard>().thisCard = Deck.Instance.cardList[4];
        temp.name = "Card[Slime]";
        for (int j = 0; j < temp.transform.childCount; j++)
        {
            switch (j)
            {
                case 0:
                    temp.transform.GetChild(j).name = "Card[Slime]_CostImg";
                    temp.transform.GetChild(j).GetChild(0).name = "Card[Slime]_CostText";
                    break;
                case 1:
                    temp.transform.GetChild(j).name = "Card[Slime]_CardTitle";
                    break;
                case 2:
                    temp.transform.GetChild(j).name = "Card[Slime]_CardText";
                    break;
                case 3:
                    temp.transform.GetChild(j).name = "Card[Slime]_CardImg";
                    break;
                case 4:
                    temp.transform.GetChild(j).name = "Card[Slime]_CardType";
                    break;
            }
        }//카드정보설정
        beforUse.Add(temp);
        ShuffleDeck(beforUse);
    }
    void DieMonster()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].GetComponent<Monster>().stat.hp <= 0)
            {
                monsters[i].SetActive(false);
            }
        }
    }
    public bool EndBattle()//업데이트 체크
    {
        bool playerCheck = false;
        bool isEnd = false;
        List<bool> monsterCheck = new List<bool>();
        if (stat.hp > 0)
        {
            playerCheck = true;
        }
        else
        {
            playerCheck = false;
        }
        for (int i = 0; i < monsters.Count; i++)
        {
            monsterCheck.Add(false);
        }
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].GetComponent<Monster>().stat.hp > 0)
            {
                monsterCheck[i] = true;
            }
            else
            {
                monsterCheck[i] = false;
            }
        }

        for(int i = 0; i < monsterCheck.Count; i++){
            if(monsterCheck[i]== true)
            {
                isEnd = false;
                result = BattleResult.Lose;
                break;
            }
            else
            {
                result = BattleResult.Win;
                isEnd = true;
                if (i == monsterCheck.Count - 1)
                {
                    return isEnd;
                }
            }
        }
        if (playerCheck == false)
        {
            result = BattleResult.Lose;
            isEnd = true;
        }
        else
        {
            result = BattleResult.Win;
            isEnd = false;
        }
        return isEnd;
    }
}
