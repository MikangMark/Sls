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
    List<CardInfo> battleDeck;//�������� ����� ���� ��

    List<CardInfo> beforUse;//���� ī�����
    List<CardInfo> afterUse;//�����ī�����
    List<CardInfo> myHand;//���� ����
    void OnEnable()//setactive true�ɶ� ����
    {
        //�������� ����
        //1.���ҷ����� -> ���������� -> ĳ������ �ҷ�����
        initData();
        
    }
    void Update()
    {
        if (myTurn)//���� ��
        {
            //CardDraw(divideCard);
        }
        else//���� ��
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

    void CardDraw(int divide)//ī�带 �����ٶ����� ����
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
