using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerBuffType { DEFAULT = 0, ATK, DEF, POW, WEAK }
public class Battle : MonoBehaviour
{
    int divideCard;
    public int maxEnergy;
    public int energy;
    int refillEnergy;
    int shiled;
    public Character.CharInfo stat;//전투중인 나의 스텟
    public Dictionary<PlayerBuffType, int> playerBufList;
    public List<Monster> monsters;//전투중인 적의 리스트
    List<CardInfo> battleDeck;//전투에서 사용할 나의 덱

    List<CardInfo> beforUse;//뽑을 카드모음
    List<CardInfo> afterUse;//사용한카드모음
    List<CardInfo> myHand;//나의 손패카드정보
    List<GameObject> myCard;//나의 손패 오브젝트
    public GameObject myCardParent;

    public GameObject cardPrf;
    void OnEnable()//setactive true될때 실행
    {
        //전투시작 셋팅
        //1.덱불러오기 -> 에너지셋팅 -> 캐릭스텟 불러오기
        initData();
        
    }
    void initData()
    {
        stat = new Character.CharInfo();

        battleDeck = new List<CardInfo>(Deck.Instance.deck);

        beforUse = new List<CardInfo>(battleDeck);
        myHand = new List<CardInfo>();
        afterUse = new List<CardInfo>();
        myCard = new List<GameObject>();
        playerBufList = new Dictionary<PlayerBuffType, int>();
        playerBufList.Add(PlayerBuffType.POW, 0);
        playerBufList.Add(PlayerBuffType.WEAK, 0);
        stat = InGame.Instance.charInfo;
        shiled = 0;
        divideCard = 5;
        maxEnergy = 3;
        energy = maxEnergy;
        refillEnergy = maxEnergy;
        MyTurn();
    }
    void MyTurn()
    {
        ShuffleDeck(beforUse);
        CardDraw(divideCard);
        ReChargeEnergy(refillEnergy);
    }
    public void EndMyTurn()
    {

    }
    public void EnemyTurn()
    {

    }
    public  void EndEnemyTurn()
    {

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
        //Debug.Log(beforUse[0].title);
        //Debug.Log(beforUse[divide - 1].title);

        GameObject temp;
        for (int i = 0; i < divide; i++)
        {
            myHand.Add(beforUse[i]);                                      //드로우할카드 정보 저장
            temp = Instantiate(cardPrf);
            temp.name = "Card[" + i + "]";
            for(int j=0; j<temp.transform.childCount; j++)
            {
                switch (j)
                {
                    case 0:
                        temp.transform.GetChild(j).name = "CostImg" + i;
                        temp.transform.GetChild(j).GetChild(0).name = "CostText" + i;
                        break;
                    case 1:
                        temp.transform.GetChild(j).name = "CardTitle" + i;
                        break;
                    case 2:
                        temp.transform.GetChild(j).name = "CardText" + i;
                        break;
                    case 3:
                        temp.transform.GetChild(j).name = "CardImg" + i;
                        break;
                    case 4:
                        temp.transform.GetChild(j).name = "CardType" + i;
                        break;
                }
            }
            temp.GetComponent<OneCard>().thisCard = myHand[i];
            temp.transform.parent = myCardParent.transform;
            myCard.Add(temp);                             //정보가들어간 카드오브젝트 내손에 넣기
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
        /*
        switch (target.tag)
        {
            case "Monster":
                target.GetComponent<Monster>().bufList[MonsterBuffType.POW]++;
                break;

            case "Player":

                break;
        }
        */
    }
    public void Weak(GameObject target, int value)
    {
        target.GetComponent<Monster>().bufList[MonsterBuffType.WEAK]++;
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

    }
    public void MonsterPow(Monster monsterObj, int value)
    {

    }
}
