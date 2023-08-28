using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : Singleton<InGame>
{
    public Character.CharInfo charInfo;
    public bool openDeckView;
    public int currentFloor = 0;

    public int rewardGold;
    public int rewardCardGroup;
    public List<List<CardInfo>> rewardCards;

    public GameObject disCardWarringWiew;
    public ShopScript shopscript;
    private void Awake()
    {
        Init();
        openDeckView = false;
        charInfo = Character.Instance.ironclead;
        //charInfo = Character.Instance.ironclead;
        switch (PlayerPrefs.GetInt("CharType")) //��â���������� ĳ��
        {
            case 0:
                charInfo = Character.Instance.ironclead;//���̾�Ŭ����
                break;
            case 1:
                charInfo = Character.Instance.silence;//���Ϸ���
                break;
            case 2:
                charInfo = Character.Instance.defact;//����Ʈ
                break;
            case 3:
                charInfo = Character.Instance.wacher;//����
                break;
            default:
                charInfo = Character.Instance.ironclead;//���̾�Ŭ����
                break;
        }
    }
    private void Start()
    {
        //SetReward();
    }

    public void SetReward()
    {
        rewardGold = CreateSeed.Instance.RandNum(50, 100);
        rewardCardGroup = CreateSeed.Instance.RandNum(1, 3);
        rewardCards = new List<List<CardInfo>>();
        for (int i = 0; i < rewardCardGroup; i++)
        {
            rewardCards.Add(new List<CardInfo>());
            for (int j = 0; j < 3; j++)
            {
                int cardnumRand = CreateSeed.Instance.RandNum(0, Deck.Instance.cardList.Count - 1);
                if(cardnumRand == 4)
                {
                    j--;
                }
                else
                {
                    rewardCards[i].Add(Deck.Instance.cardList[cardnumRand]);
                }
            }
        }
    }


}
