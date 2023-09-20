using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class OneCard : MonoBehaviour
{
    public CardInfo thisCard;//key
    //public CardValue thisCardValue;//value
    [SerializeField]
    private ExcelDataLoader cardData;
    public Battle battle;
    public TextMeshProUGUI cCost;
    public TextMeshProUGUI cTitle;
    public TextMeshProUGUI cType;
    public TextMeshProUGUI cText;
    public Image cImg;
    
    private void Start()
    {
        cardData = GameObject.Find("ExcelData").GetComponent<ExcelDataLoader>();
        if(GameObject.Find("BattleScript") != null)
        {
            battle = GameObject.Find("BattleScript").GetComponent<Battle>();
        }
    }
    private void FixedUpdate()
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
        if (gameObject.transform.parent.tag.Equals("Shop"))
        {
            GetComponent<MousePoint>().enabled = false;
        }
    }
    public void SetCard(CardInfo _card)
    {
        cardData = GameObject.Find("ExcelData").GetComponent<ExcelDataLoader>();
        thisCard = _card;
        //thisCardValue = cardData.allInfoCard[_card];
    }

    protected List<Action<GameObject>> m_CallFNList = new List<Action<GameObject>>();


    protected void Call_PlayerDedefaultFN(GameObject target )
    {

    }
    void InitSettings()
    {
        m_CallFNList = new List<Action<GameObject>>((int)CardInfo.CardType.MAX);

        m_CallFNList[(int)CardInfo.CardType.DEFAULT] = Call_PlayerDedefaultFN;
        m_CallFNList[(int)CardInfo.CardType.ATK] = null;
    }


    public bool UseThisCard(GameObject target)//카드사용의 성공했으면 true
    {
        int value = 0;
        CardInfo.Type[] sk_Temp = null;
        if (battle.energy >= thisCard.cost)
        {
            if (!CardType(sk_Temp, value, target))
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        battle.UsedCardMove(gameObject);
        return true;
    }

    bool CardType(CardInfo.Type[] types,int value,GameObject target)
    {
        if (thisCard.subType == CardInfo.Type.DEFAULT)
        {
            types = new CardInfo.Type[1];
            types[0] = thisCard.type;
        }
        else
        {
            types = new CardInfo.Type[2];
            types[0] = thisCard.type;
            types[1] = thisCard.subType;
        }
        battle.energy -= thisCard.cost;
        for (int i = 0; i < types.Length; i++)
        {
            switch (types[i])
            {
                case CardInfo.Type.DEFAULT:
                    return false;
                case CardInfo.Type.ATK:
                    battle.Attack(target, thisCard.skillValue[CardInfo.Type.ATK]);
                    break;
                case CardInfo.Type.DEF:
                    battle.Deffence(target, thisCard.skillValue[CardInfo.Type.DEF]);
                    break;
                case CardInfo.Type.POW:
                    battle.Power(target, thisCard.skillValue[CardInfo.Type.POW]);
                    break;
                case CardInfo.Type.WEAK:
                    battle.Weak(target, thisCard.skillValue[CardInfo.Type.WEAK]);
                    break;
                case CardInfo.Type.EXTINCTION:
                    battle.DeleteCardMove(gameObject);
                    break;
            }
        }
        return true;
    }
    
}
