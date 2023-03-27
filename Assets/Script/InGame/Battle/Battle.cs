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
    List<CardInfo> battleDeck;//���ǵ�

    List<CardInfo> drawUse;//���� ī�����
    List<CardInfo> afterUse;//�����ī�����
    List<CardInfo> myHand;//���� ����
    // Start is called before the first frame update
    void Start()
    {
        //�������� ����
        //1.���ҷ����� -> ���������� -> ĳ������ �ҷ�����
        initData();
    }

    // Update is called once per frame
    void Update()
    {
        if (myTurn)//���� ��
        {
            CardDivide(divideCard);
        }
        else//���� ��
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

    void CardDivide(int divide)//ī�带 �����ٶ����� ����
    {

    }
}
