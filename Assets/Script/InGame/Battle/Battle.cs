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
    List<CardInfo> battleDeck;//�������� ����� ���� ��

    List<CardInfo> beforUse;//���� ī�����
    List<CardInfo> afterUse;//�����ī�����
    List<CardInfo> myHand;//���� ����ī������
    List<GameObject> myCard;//���� ���� ������Ʈ
    public GameObject myCardParent;

    public GameObject cardPrf;
    void OnEnable()//setactive true�ɶ� ����
    {
        //�������� ����
        //1.���ҷ����� -> ���������� -> ĳ������ �ҷ�����
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
    public void ShuffleDeck<T>(List<T> list)//ī�� ����
    {
        // ����Ʈ�� ��� ���� 2 �̻��� ���� ���� ����
        if (list.Count > 1)
        {
            int n = list.Count;
            // ����Ʈ�� �����ϰ� ���� ���� Fisher-Yates �˰����� ���
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
    void CardDraw(int divide)//ī�带 �����ٶ����� ����
    {
        //Debug.Log(beforUse[0].title);
        //Debug.Log(beforUse[divide - 1].title);
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < divide; i++)
        {
            myHand.Add(beforUse[i]);                                      //��ο���ī�� ���� ����
            cardPrf.GetComponent<OneCard>().thisCard = myHand[i];       //���������� ī�������Ʈ���ֱ�
            tempList.Add(Instantiate(cardPrf));
            tempList[i].transform.parent = myCardParent.transform;
            myCard.Add(tempList[i]);                             //�������� ī�������Ʈ ���տ� �ֱ�
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
