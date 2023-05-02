using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerBuffType { POW = 0, WEAK }
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
    public Character.CharInfo stat;//�������� ���� ����
    public Dictionary<PlayerBuffType, int> playerBufList;//�÷��̾� ���� ����Ʈ
    public List<Monster> monsters;//�������� ���� ����Ʈ
    List<GameObject> battleDeck;//�������� ����� ���� ��

    public List<GameObject> beforUse;//���� ī�����
    public List<GameObject> afterUse;//����� ī�����
    public List<GameObject> deletCard;//�Ҹ�� ī�����
    public List<GameObject> myHand;//���� ����ī�����

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

        battleDeck = new List<GameObject>(Deck.Instance.cardList_Obj);

        beforUse = new List<GameObject>(battleDeck);
        myHand = new List<GameObject>();
        afterUse = new List<GameObject>();
        deletCard = new List<GameObject>();
        playerBufList = new Dictionary<PlayerBuffType, int>();
        playerBufList.Add(PlayerBuffType.POW, 0);
        playerBufList.Add(PlayerBuffType.WEAK, 0);
        stat = InGame.Instance.charInfo;
        shiled = 0;
        divideCard = 5;
        maxEnergy = 3;
        energy = maxEnergy;
        refillEnergy = maxEnergy;
        thisTurn = Turn.Player;
        MyTurn();
    }
    
    void MyTurn()
    {
        ShuffleDeck(beforUse);
        CardDraw(divideCard);
        ReChargeEnergy(refillEnergy);
    }
    public void EndMyTurn()//�����ᴭ������
    {
        //������� �÷��̾����� ī��Ʈ ����
        //���� ����

        EnemyTurn();
    }
    public void EnemyTurn()
    {
        //�ǵ���� �ش� �ִϸ��̼� ����� ���� �Լ��̵�
        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        //���� ����, ����� ī��Ʈ����
        MyTurn();
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
        for (int i = 0; i < divide; i++)
        {
            GameObject temp = beforUse[i];
            temp.GetComponent<OneCard>().code -= battleDeck.Count;
            temp.name = "Hand_Card[" + i + "]";
            for (int j = 0; j < temp.transform.childCount; j++)
            {
                switch (j)
                {
                    case 0:
                        temp.transform.GetChild(j).name = "Hand_CostImg" + i;
                        temp.transform.GetChild(j).GetChild(0).name = "Hand_CostText" + i;
                        break;
                    case 1:
                        temp.transform.GetChild(j).name = "Hand_CardTitle" + i;
                        break;
                    case 2:
                        temp.transform.GetChild(j).name = "Hand_CardText" + i;
                        break;
                    case 3:
                        temp.transform.GetChild(j).name = "Hand_CardImg" + i;
                        break;
                    case 4:
                        temp.transform.GetChild(j).name = "Hand_CardType" + i;
                        break;
                }
            }
            myHand.Add(Instantiate(temp, myCardParent.transform));                             //�������� ī�������Ʈ ���տ� �ֱ�

            beforUse.RemoveAt(i);
        }
    }
    public void UsedCardMove(OneCard target)
    {
        for(int i = 0; i < myHand.Count; i++)
        {
            
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
        monsterObj.stat.shield += value;
    }
    public void MonsterPow(Monster monsterObj, int value)
    {
        monsterObj.bufList[MonsterBuffType.POW] += value;
    }
}
