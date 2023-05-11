using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerBuffType { POW = 0, WEAK, VULNER, IMPAIR, SLIMECARD, RESTRAINT, CONSCIOUS }
public class Battle : MonoBehaviour
{
    enum Turn { Player = 0, EndPlayer, Enermy, EnermyEnd }
    [SerializeField]
    Turn thisTurn;
    int divideCard;
    public int maxEnergy;
    public int energy;
    int refillEnergy;
    public int shiled;
    public Character.CharInfo stat;//전투중인 나의 스텟
    public Dictionary<PlayerBuffType, int> playerBufList;//플레이어 버프 리스트
    public List<GameObject> monsters;//전투중인 적의 리스트
    List<GameObject> battleDeck;//전투에서 사용할 나의 덱

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
    void OnEnable()//setactive true될때 실행
    {
        //전투시작 셋팅
        //1.덱불러오기 -> 에너지셋팅 -> 캐릭스텟 불러오기
        initData();
        
    }
    
    void initData()
    {
        stat = new Character.CharInfo();
        Instantiate(playerPrf, playerPos.transform);
        battleDeck = new List<GameObject>(Deck.Instance.cardList_Obj);
        monsters = new List<GameObject>();
        oneMonster = new List<string[]>();
        for (int i = 0; i < monsterGrup.Count; i++)
        {
            oneMonster.Add(monsterGrup[i].Split(','));
        }
        CreateEnemy();
        beforUse = new List<GameObject>(battleDeck);
        myHand = new List<GameObject>();
        afterUse = new List<GameObject>();
        deletCard = new List<GameObject>();
        playerBufList = new Dictionary<PlayerBuffType, int>();
        //playerBufList.Add(PlayerBuffType.POW, 0);
        //playerBufList.Add(PlayerBuffType.WEAK, 0);
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
        thisTurn = Turn.Player;
        MyTurn();
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
        CardDraw(divideCard);
        CreateBeforCardObj();
        ReChargeEnergy(refillEnergy);
    }
    public void EndMyTurn()//턴종료눌렀을떄
    {
        //턴종료시 플레이어디버프 카운트 감소
        //쉴드 제거

        EnemyTurn();
    }
    public void EnemyTurn()
    {
        //의도대로 해당 애니매이션 실행뒤 다음 함수이동
        for(int i=0;i< monsters.Count; i++)
        {
            monsters[i].GetComponent<Monster>().PlaySkill();
        }
        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        #region 몬스터버프감소
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].GetComponent<Monster>().shiled = 0;
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
    void CardDraw(int divide)//카드를 나눠줄때마다 실행
    {
        if (beforUse.Count < divide)//뽑을곳의 카드의 갯수가 뽑을카드갯수보다 작을때 실행
        {
            for (int i = 0; i < beforUse.Count; i++)
            {
                GameObject temp = beforUse[i];
                myHand.Add(Instantiate(temp, myCardParent.transform));                             //정보가들어간 카드오브젝트 내손에 넣기
                beforUse.RemoveAt(i);
            }
            beforUse = afterUse;
            ShuffleDeck(beforUse);
            beforUse.Clear();
            for(int i=0;i< beforContent.transform.childCount; i++)
            {
                Destroy(beforContent.transform.GetChild(i));
            }
            CardDraw(divide - beforUse.Count);
            CreateBeforCardObj();
        }
        else
        {
            for (int i = 0; i < divide; i++)
            {
                GameObject temp = beforUse[i];
                myHand.Add(Instantiate(temp, myCardParent.transform));                             //정보가들어간 카드오브젝트 내손에 넣기
                beforUse.RemoveAt(i);
            }
        }
    }
    public void UsedCardMove(GameObject target)
    {
        for(int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i] == target)
            {
                afterUse.Add(myHand[i]);
                CreateAfterCardObj(myHand[i]);
                myHand.RemoveAt(i);
                Destroy(target);
            }
        }
    }
    void ReChargeEnergy(int reEnergy)
    {
        energy = reEnergy;
    }

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
        if (target.GetComponent<Monster>().stat.shield > 0)//타겟의 몬스터가 쉴드를 가지고 있는가
        {
            if (target.GetComponent<Monster>().stat.shield >= damage)
            {
                target.GetComponent<Monster>().stat.shield -= damage;
            }
            else
            {
                damage -= target.GetComponent<Monster>().stat.shield;
                target.GetComponent<Monster>().stat.shield = 0;
            }
        }
        target.GetComponent<Monster>().stat.hp -= damage;
    }
    public void Deffence(int value)
    {
        shiled += value;
    }
    public void Power(GameObject target, int value)//아직 힘카드 없음
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
    public void CreateBeforCardObj()
    {
        for (int i = 0; i < beforUse.Count; i++)
        {
            Instantiate(beforUse[i], beforContent.transform);
        }
    }
    public void CreateAfterCardObj(GameObject _card)
    {
        Instantiate(_card, afterContent.transform);
    }
    public void CreateSlimeCardObj()
    {
        //beforUse.Add(Instantiate())cardList
        GameObject temp;
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
        }
        beforUse.Add(temp);
    }
}
