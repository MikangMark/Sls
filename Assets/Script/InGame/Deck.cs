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

    // Start is called before the first frame update

    void Start()
    {
        cardList = new List<CardInfo>();
        for(int i = 0; i < excelData.cardInfo.Count; i++)
        {
            cardList.Add(excelData.cardInfo[i]);
        }

        deck = new List<CardInfo>();
        for(int i = 0; i < attack; i++)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
