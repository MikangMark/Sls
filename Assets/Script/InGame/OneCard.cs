using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneCard : MonoBehaviour
{
    public CardInfo thisCard;//key
    public CardValue thisCardValue;//value
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
        battle = GameObject.Find("BattleScript").GetComponent<Battle>();
        thisCardValue = cardData.allInfoCard[thisCard];

    }
    private void FixedUpdate()
    {
        cCost.text = thisCard.cost.ToString();
        cTitle.text = thisCard.title.ToString();
        cType.text = thisCard.type.ToString();
        cText.text = thisCard.text.ToString();
        cImg.sprite = thisCard.cardImg;

        if (InGame.Instance.openDeckView)
        {
            GetComponent<MousePoint>().enabled = false;
        }
        else
        {
            GetComponent<MousePoint>().enabled = true;
        }
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
