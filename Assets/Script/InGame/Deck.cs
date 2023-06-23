using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Singleton<Deck>
{
    public ExcelDataLoader excelData;
    public List<CardInfo> cardList;
    public List<CardInfo> deck;
    [SerializeField]
    int attack = 5;
    [SerializeField]
    int defence = 4;
    [SerializeField]
    int blow = 1;
    [SerializeField]
    int ignition = 1;
    public List<GameObject> cardList_Obj;

    [SerializeField]
    GameObject cardPrf;
    [SerializeField]
    GameObject content;

    // Start is called before the first frame update

    void Awake()
    {
        Init();
        cardList = new List<CardInfo>();
        for(int i = 0; i < excelData.cardInfo.Count; i++)
        {
            cardList.Add(excelData.cardInfo[i]);
        }

        deck = new List<CardInfo>();
        cardList_Obj = new List<GameObject>();
        for (int i = 0; i < attack; i++)
        {
            deck.Add(cardList[0]);
        }
        for (int i = 0; i < defence; i++)
        {
            deck.Add(cardList[1]);
        }
        for (int i = 0; i < blow; i++)
        {
            deck.Add(cardList[2]);
        }
        for (int i = 0; i < ignition; i++)
        {
            deck.Add(cardList[3]);
        }
    }

    public void AddDeck(CardInfo info)
    {
        deck.Add(info);
        GameObject temp;
        temp = Instantiate(cardPrf, content.transform);
        temp.GetComponent<OneCard>().thisCard = info;
        temp.name = "Card[" + (deck.Count - 1) + "]";
        for (int j = 0; j < temp.transform.childCount; j++)
        {
            switch (j)
            {
                case 0:
                    temp.transform.GetChild(j).name = "CostImg" + (deck.Count - 1);
                    temp.transform.GetChild(j).GetChild(0).name = "CostText" + (deck.Count - 1);
                    break;
                case 1:
                    temp.transform.GetChild(j).name = "CardTitle" + (deck.Count - 1);
                    break;
                case 2:
                    temp.transform.GetChild(j).name = "CardText" + (deck.Count - 1);
                    break;
                case 3:
                    temp.transform.GetChild(j).name = "CardImg" + (deck.Count - 1);
                    break;
                case 4:
                    temp.transform.GetChild(j).name = "CardType" + (deck.Count - 1);
                    break;
            }
        }
        cardList_Obj.Add(temp);
    }
}
