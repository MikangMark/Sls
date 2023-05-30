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
    public Character.CharInfo stat;//�������� ���� ����
    public Dictionary<PlayerBuffType, int> playerBufList;//�÷��̾� ���� ����Ʈ
    public List<GameObject> monsters;//�������� ���� ����Ʈ
    List<GameObject> battleDeck;//�������� ����� ���� ��
    List<GameObject> abnomalDeck;//�����̻� ī�� ��

    public List<GameObject> beforUse;//���� ī�����
    public List<GameObject> afterUse;//����� ī�����
    public List<GameObject> deletCard;//�Ҹ�� ī�����
    public List<GameObject> myHand;//���� ����ī�����
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

    [SerializeField]
    List<GameObject> checkList;

    [SerializeField]
    GameObject slimeCardObj;

    public bool thisActive = false;
    void OnEnable()//setactive true�ɶ� ����
    {
        thisActive = true;
        //�������� ����
        //1.���ҷ����� -> ���������� -> ĳ������ �ҷ�����
        initData();
        
    }
    private void OnDisable()
    {
        thisActive = false;
    }

    void initData()
    {
        slimeCardObj = new GameObject();
        abnomalDeck = new List<GameObject>();
        checkList = new List<GameObject>();
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
        for(int i = 0; i < battleDeck.Count; i++)
        {
            beforUse.Add(Instantiate(battleDeck[i], beforContent.transform));
        }
        myHand = new List<GameObject>();
        afterUse = new List<GameObject>();
        deletCard = new List<GameObject>();
        playerBufList = new Dictionary<PlayerBuffType, int>();
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
        for (int i = 0; i < divideCard; i++)
        {
            CardDraw();
        }
        ReChargeEnergy(refillEnergy);
    }
    public void EndMyTurn()//�����ᴭ������
    {
        //������� �÷��̾����� ī��Ʈ ����
        //���� ����
        //�������ִ� �� ���δ� ������
        int del = myHand.Count;
        for (int i = 0; i < del; i++)
        {
            UsedCardMove(myHand[0]);
        }
        EnemyTurn();
    }
    public void EnemyTurn()
    {
        //�ǵ���� �ش� �ִϸ��̼� ����� ���� �Լ��̵�
        for(int i=0;i< monsters.Count; i++)
        {
            monsters[i].GetComponent<Monster>().PlaySkill();
            monsters[i].GetComponent<Monster>().NextUseSkill();
        }
        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        #region ���͹�������
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].GetComponent<Monster>().stat.shield = 0;
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
        #region �÷��̾���� ����
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

    void CardDraw()//ī�带 �����ٶ����� ����
    {
        if (beforUse.Count <= 0)
        {
            beforUse.Clear();
            for(int i = 0; i < afterUse.Count; i++)
            {
                afterUse[i].transform.parent = beforContent.transform;
                beforUse.Add(afterUse[i]);
            }
            afterUse.Clear();
        }
        GameObject drawCard = beforUse[0];
        drawCard.transform.parent = myCardParent.transform;
        myHand.Add(drawCard);
        beforUse.RemoveAt(0);
    }
    public void UsedCardMove(GameObject target)
    {
        //GameObject temp = CreateAfterCardObj(target);
        target.transform.parent = afterContent.transform;
        afterUse.Add(target);
        for (int i = 0; i < myHand.Count; i++)
        {
            if(myHand[i] == target)
            {
                myHand.RemoveAt(i);
                break;
            }
        }
    }
    public void DeleteCardMove(GameObject target)
    {
        //GameObject temp = CreateDeleteCardObj(target);
        target.transform.parent = deleteContent.transform;
        deletCard.Add(target);
        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i] == target)
            {
                myHand.RemoveAt(i);
                break;
            }
        }
    }
    void ReChargeEnergy(int reEnergy)
    {
        energy = reEnergy;
    }
    #region ������ũ��Ʈ
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
        target.GetComponent<Monster>().bufList[MonsterBuffType.WEAK] += value;
    }
    #endregion
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
    public void CreateSlimeCardObj()
    {
        GameObject temp = new GameObject();
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
        }//ī����������
        beforUse.Add(temp);
        ShuffleDeck(beforUse);
    }
}
