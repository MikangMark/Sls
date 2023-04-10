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
    public Character.CharInfo stat;//�������� ���� ����
    public Dictionary<PlayerBuffType, int> playerBufList;
    public List<Monster> monsters;//�������� ���� ����Ʈ
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

        GameObject temp;
        for (int i = 0; i < divide; i++)
        {
            myHand.Add(beforUse[i]);                                      //��ο���ī�� ���� ����
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
            myCard.Add(temp);                             //�������� ī�������Ʈ ���տ� �ֱ�
        }
    }
    void ReChargeEnergy(int reEnergy)
    {
        energy = reEnergy;
    }

    public void Attack(GameObject target, int value)
    {
        int damage = value;
        if (playerBufList[PlayerBuffType.POW] > 0)//�÷��̾ �������� �ް��ִ°�
        {
            damage += playerBufList[PlayerBuffType.POW];
        }
        if (target.GetComponent<Monster>().bufList[MonsterBuffType.WEAK] > 0)//Ÿ���� ���Ͱ� ��� ������� �ް��ִ°�
        {
            damage = damage + (int)(damage * 0.5);
        }
        if (target.GetComponent<Monster>().stat.shield > 0)//Ÿ���� ���Ͱ� ���带 ������ �ִ°�
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
    public void Power(GameObject target, int value)//���� ��ī�� ����
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
