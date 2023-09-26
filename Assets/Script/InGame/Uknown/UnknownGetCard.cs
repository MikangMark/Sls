using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownGetCard : MonoBehaviour
{
    [SerializeField]
    GameObject getCardView;
    [SerializeField]
    GameObject getCardContent;
    [SerializeField]
    GameObject confirmationView;
    public List<OneCard> cardList;
    public List<CardInfo> randCardList;

    // Start is called before the first frame update
    void OnEnable()
    {
        InitSetGetCard();
    }
    void InitSetGetCard()
    {
        randCardList = CreateSeed.Instance.GetRandomValue<CardInfo>(Deck.Instance.cardList, 16);
        for(int i = 0; i < cardList.Count; i++)
        {
            cardList[i].thisCard = randCardList[i];
        }
    }
}
