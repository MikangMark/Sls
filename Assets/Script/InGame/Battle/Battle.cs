using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    int divideCard;
    int maxEnergy;
    int energy;
    public Character.CharInfo stat;
    bool myTurn;
    List<CardInfo> battleDeck;//전투에서 사용할 나의 덱

    List<CardInfo> beforUse;//뽑을 카드모음
    List<CardInfo> afterUse;//사용한카드모음
    List<CardInfo> myHand;//나의 손패
    void OnEnable()//setactive true될때 실행
    {
        //전투시작 셋팅
        //1.덱불러오기 -> 에너지셋팅 -> 캐릭스텟 불러오기
        initData();
        
    }
    void Update()
    {
        if (myTurn)//나의 턴
        {
            //CardDraw(divideCard);
        }
        else//적의 턴
        {

        }
    }

    void initData()
    {
        stat = new Character.CharInfo();

        battleDeck = new List<CardInfo>(Deck.Instance.deck);

        beforUse = new List<CardInfo>(battleDeck);
        myHand = new List<CardInfo>();
        afterUse = new List<CardInfo>();
        

        stat = InGame.Instance.charInfo;
        myTurn = true;
        divideCard = 5;
        maxEnergy = 3;
        energy = maxEnergy;
    }

    void CardDraw(int divide)//카드를 나눠줄때마다 실행
    {
        for(int i = 0; i < divide; i++)
        {
            myHand.Add(beforUse[i]);
        }
    }

    void UseCard(CardInfo use)
    {

    }
}
