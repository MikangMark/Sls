using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public List<GameObject> shopList;
    public GameObject cardPrf;
    [SerializeField]
    GameObject shopping;
    [SerializeField]
    GameObject disConten;
    [SerializeField]
    GameObject disCardView;
    [SerializeField]
    int discardPay;
    [SerializeField]
    TextMeshProUGUI disCardPayText;

    [SerializeField]
    CardInfo disCardTarget;
    [SerializeField]
    GameObject disCardWarringView;

    [SerializeField]
    GameObject content;
    // Start is called before the first frame update
    void OnEnable()
    {
        InitShopList();
        CreateDisCardDeckObj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        disCardPayText.text = discardPay.ToString();
    }
    void InitShopList()
    {
        shopping.SetActive(true);
        disCardView.SetActive(false);
        for (int i = 0; i < shopList.Count; i++)
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
    public void CreateDisCardDeckObj()
    {
        GameObject temp;
        for (int i = 0; i < Deck.Instance.deck.Count; i++)
        {
            temp = Instantiate(cardPrf, disConten.transform);
            temp.GetComponent<OneCard>().thisCard = Deck.Instance.deck[i];
            temp.name = "Card[" + i + "]";
            for (int j = 0; j < temp.transform.childCount; j++)
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
        }
    }
    public void DisCardViewBtn()
    {
        if (InGame.Instance.charInfo.money >= 75)
        {
            disCardView.SetActive(true);
        }
        else
        {
            Debug.Log("소지금 부족");
        }
        
    }
    public void SetDisCardTarget(CardInfo target)
    {
        disCardTarget = target;
    }
    public void OnClickYesDisCard()
    {
        Debug.Log(content.transform.childCount);
        if (InGame.Instance.charInfo.money < 75)
        {
            Debug.Log("소지금 부족");
            return;
        }
        if (disCardTarget != null)
        {
            for(int i = 0; i < Deck.Instance.deck.Count; i++)
            {
                if(Deck.Instance.deck[i] == disCardTarget)
                {
                    Deck.Instance.deck.RemoveAt(i);
                    for(int j=0;j< disConten.transform.childCount; j++)
                    {
                        if(disConten.transform.GetChild(j).GetComponent<OneCard>().thisCard == disCardTarget)
                        {
                            Destroy(disConten.transform.GetChild(j).gameObject);
                            break;
                        }
                        
                    }
                    for (int j = 0; j < content.transform.childCount; j++)
                    {
                        if (content.transform.GetChild(j).GetComponent<OneCard>().thisCard == disCardTarget)
                        {
                            Destroy(content.transform.GetChild(j).gameObject);
                            break;
                        }
                    }

                }
                
            }
            InGame.Instance.charInfo.money -= 75;
            disCardWarringView.SetActive(false);
        }
    }
    public void OnClickNoDisCard()
    {
        disCardWarringView.SetActive(false);
    }
}
