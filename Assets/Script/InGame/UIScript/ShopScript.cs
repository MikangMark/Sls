using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public List<GameObject> shopList;
    public GameObject sellCard;

    // Start is called before the first frame update
    void OnEnable()
    {
        InitShopList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitShopList()
    {
        for(int i = 0; i < shopList.Count; i++)
        {
            int randnum = CreateSeed.Instance.RandNum(0, Deck.Instance.cardList.Count - 1);
            if(Deck.Instance.cardList[randnum].shop == 0)
            {
                i--;
                continue;
            }
            shopList[i].GetComponent<OneCard>().thisCard = Deck.Instance.cardList[randnum];
            shopList[i].transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = shopList[i].GetComponent<OneCard>().thisCard.shop.ToString();
        }
    }

    void DisCardBtn()
    {

    }
}
