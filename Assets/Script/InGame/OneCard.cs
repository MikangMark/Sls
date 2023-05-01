using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneCard : MonoBehaviour
{
    [SerializeField]
    public static int value = 0;
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
        cCost = GameObject.Find("CostText" + value).GetComponent<TextMeshProUGUI>();
        cTitle = GameObject.Find("CardTitle" + value).GetComponent<TextMeshProUGUI>();
        cType = GameObject.Find("CardType" + value).GetComponent<TextMeshProUGUI>();
        cText = GameObject.Find("CardText" + value).GetComponent<TextMeshProUGUI>();
        cImg = GameObject.Find("CardImg" + value).GetComponent<Image>();
        battle = GameObject.Find("BattleScript").GetComponent<Battle>();
        thisCardValue = cardData.allInfoCard[thisCard];
        Debug.Log(value);
    }
    private void FixedUpdate()
    {
        cCost.text = thisCard.cost.ToString();
        cTitle.text = thisCard.title.ToString();
        cType.text = thisCard.type.ToString();
        cText.text = thisCard.text.ToString();
        cImg.sprite = thisCard.cardImg;
    }

    public bool UseThisCard(GameObject target)//카드사용의 성공했으면 true
    {
        if (battle.energy >= thisCard.cost)
        {
            battle.energy -= thisCard.cost;
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
                        battle.Attack(target, thisCardValue.skillValue[thisCardValue.type[i]]);

                        break;
                    case CardType.DEF:
                        if (target.tag.Equals("Monster"))
                        {
                            return false;
                        }
                        battle.Deffence(thisCardValue.skillValue[thisCardValue.type[i]]);
                        break;
                    case CardType.POW://아직 카드없음
                        if (target.tag.Equals("Monster"))
                        {
                            return false;
                        }
                        battle.Power(target, thisCardValue.skillValue[thisCardValue.type[i]]);

                        break;
                    case CardType.WEAK:
                        if (target.tag.Equals("Player"))
                        {
                            return false;
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
        return false;
    }
}
