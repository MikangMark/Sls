using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneCard : MonoBehaviour
{
    public CardInfo thisCard;//key
    public CardValue thisCardValue;//value
    [SerializeField]
    private CardValueExcelDataLoader cardData;
    public Battle battle;
    public TextMeshProUGUI cCost;
    public TextMeshProUGUI cTitle;
    public TextMeshProUGUI cType;
    public TextMeshProUGUI cText;
    public Image cImg;
    
    private void Start()
    {
        cardData = GameObject.Find("ExcelData").GetComponent<CardValueExcelDataLoader>();
        if(GameObject.Find("BattleScript") != null)
        {
            battle = GameObject.Find("BattleScript").GetComponent<Battle>();
        }
        if (!gameObject.transform.parent.tag.Equals("Reward"))
        {
            thisCardValue = cardData.allInfoCard[thisCard];
        }
    }
    private void FixedUpdate()
    {
        if(thisCardValue != null)
        {
            cCost.text = thisCard.cost.ToString();
            cTitle.text = thisCard.title.ToString();
            cType.text = thisCard.cardType.ToString();
            cText.text = thisCard.text.ToString();
            cImg.sprite = thisCard.cardImg;

            if (!gameObject.transform.parent.tag.Equals("Reward"))
            {
                if (InGame.Instance.openDeckView)
                {
                    GetComponent<MousePoint>().enabled = false;
                }
                else
                {
                    GetComponent<MousePoint>().enabled = true;
                }
            }
        }
        
    }
    public void SetCard(CardInfo _card)
    {
        cardData = GameObject.Find("ExcelData").GetComponent<CardValueExcelDataLoader>();
        thisCard = _card;
        thisCardValue = cardData.allInfoCard[_card];
    }
    public bool UseThisCard(GameObject target)//카드사용의 성공했으면 true
    {
        if (battle.energy >= thisCard.cost)
        {
            //thisCardValue의 type값을 읽어 어떤카드인지 인식
            for (int i = 0; i < thisCardValue.type.Count; i++)
            {
                switch (thisCardValue.type[i])
                {
                    case CardType.ATK:
                        if (target.tag.Equals("Player"))
                        {
                            return false;
                        }
                        if (i == 0)
                        {
                            battle.energy -= thisCard.cost;
                        }
                        battle.Attack(target, thisCardValue.skillValue[thisCardValue.type[i]]);
                        break;
                    case CardType.DEF:
                        if (target.tag.Equals("Monster"))
                        {
                            return false;
                        }
                        if (i == 0)
                        {
                            battle.energy -= thisCard.cost;
                        }
                        battle.Deffence(thisCardValue.skillValue[thisCardValue.type[i]]);
                        break;
                    case CardType.POW:
                        if (target.tag.Equals("Monster"))
                        {
                            return false;
                        }
                        if (i == 0)
                        {
                            battle.energy -= thisCard.cost;
                        }
                        battle.Power(target, thisCardValue.skillValue[thisCardValue.type[i]]);
                        break;
                    case CardType.WEAK:
                        if (target.tag.Equals("Player"))
                        {
                            return false;
                        }
                        if (i == 0)
                        {
                            battle.energy -= thisCard.cost;
                        }
                        battle.Weak(target, thisCardValue.skillValue[thisCardValue.type[i]]);
                        break;
                    case CardType.EXTINCTION:
                        if (i == 0)
                        {
                            battle.energy -= thisCard.cost;
                        }
                        battle.DeleteCardMove(gameObject);
                        return true;
                }

            }
        }
        else
        {
            return false;
        }
        battle.UsedCardMove(gameObject);
        return true;
    }
    
}
