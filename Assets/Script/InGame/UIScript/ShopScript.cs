using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public List<GameObject> shopList;
    public GameObject sellCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitShopList()
    {
        for(int i = 0; i < shopList.Count; i++)
        {
            shopList[i].GetComponent<OneCard>().thisCard = Deck.Instance.cardList[CreateSeed.Instance.RandNum(0, Deck.Instance.cardList.Count - 1)];
        }
    }
}
