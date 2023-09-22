using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownDisCard : MonoBehaviour
{
    public GameObject cardPrf;
    [SerializeField]
    GameObject disConten;
    [SerializeField]
    GameObject disCardView;
    [SerializeField]
    CardInfo disCardTarget;
    [SerializeField]
    GameObject disCardWarringView;

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
    public void SetDisCardTarget(CardInfo target)
    {
        disCardTarget = target;
    }
    public void OnClickYesDisCard()
    {
        if (disCardTarget != null)
        {
            for (int i = 0; i < Deck.Instance.deck.Count; i++)
            {
                if (Deck.Instance.deck[i] == disCardTarget)
                {
                    Deck.Instance.deck.RemoveAt(i);
                    for (int j = 0; j < disConten.transform.childCount; j++)
                    {
                        if (disConten.transform.GetChild(j).GetComponent<OneCard>().thisCard == disCardTarget)
                        {
                            Destroy(disConten.transform.GetChild(j).gameObject);
                        }

                    }
                    for (int j = 0; j < disConten.transform.childCount; j++)
                    {
                        if (disConten.transform.GetChild(j).GetComponent<OneCard>().thisCard == disCardTarget)
                        {
                            Destroy(disConten.transform.GetChild(j).gameObject);
                        }
                    }
                }
            }
            disCardWarringView.SetActive(false);
        }
    }
    public void OnClickNoDisCard()
    {
        disCardWarringView.SetActive(false);
    }
    public void DisCardViewBtn()
    {
        disCardView.SetActive(true);
    }
    public void ExitDisCardViewBtn()
    {
        disCardView.SetActive(false);
    }
}