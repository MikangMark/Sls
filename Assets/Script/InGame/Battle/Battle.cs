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
    List<CardInfo> battleDeck;//나의덱

    List<CardInfo> drawUse;//뽑을 카드모음
    List<CardInfo> afterUse;//사용한카드모음
    List<CardInfo> myHand;//나의 손패
    // Start is called before the first frame update
    void Start()
    {
        //전투시작 셋팅
        //1.덱불러오기 -> 에너지셋팅 -> 캐릭스텟 불러오기
        initData();
    }

    // Update is called once per frame
    void Update()
    {
        if (myTurn)//나의 턴
        {
            CardDivide(divideCard);
        }
        else//적의 턴
        {

        }
    }

    void initData()
    {
        stat = new Character.CharInfo();

        battleDeck = new List<CardInfo>(Deck.Instance.deck);

        drawUse = new List<CardInfo>();
        afterUse = new List<CardInfo>();
        myHand = new List<CardInfo>();

        stat = InGame.Instance.charInfo;
        myTurn = true;
        divideCard = 5;
        maxEnergy = 3;
        energy = maxEnergy;
    }

    void CardDivide(int divide)//카드를 나눠줄때마다 실행
    {

    }
}
