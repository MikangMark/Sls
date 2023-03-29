using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    int divideCard;
    public int maxEnergy;
    public int energy;
    int refillEnergy;
    public Character.CharInfo stat;
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

        stat = InGame.Instance.charInfo;
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
    public void EnemyTurn()
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

                //
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

    void UseCard(CardInfo use)
    {

    }
}
