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
        if (battle.energy >= thisCard.cost)
        {
            switch (target.tag)
            {
                case "Player":
                    //if(m_CallFNList[(int)thisCard.cardType] != null )
                    //{
                    //    m_CallFNList[(int)thisCard.cardType](target);
                    //}
                    
                    switch (thisCard.cardType) 
                    {
                        case CardInfo.CardType.DEFAULT:
                            return false;
                        case CardInfo.CardType.ATK:
                            return false;
                        case CardInfo.CardType.SK:
                            CardInfo.Type[] sk_Temp = null;
                            if (thisCard.subType == CardInfo.Type.DEFAULT)
                            {
                                sk_Temp = new CardInfo.Type[1];
                                sk_Temp[0] = thisCard.type;
                            }
                            else
                            {
                                sk_Temp = new CardInfo.Type[2];
                                sk_Temp[0] = thisCard.type;
                                sk_Temp[1] = thisCard.subType;
                            }
                            battle.energy -= thisCard.cost;
                            for (int i = 0; i < sk_Temp.Length; i++)
                            {
                                switch (sk_Temp[i])
                                {
                                    case CardInfo.Type.DEFAULT:
                                        return false;
                                    case CardInfo.Type.ATK:
                                        return false;
                                    case CardInfo.Type.DEF:
                                        value = thisCard.skillValue[CardInfo.Type.DEF];
                                        if (battle.playerBufList[PlayerBuffType.IMPAIR] > 0)
                                        {
                                            value = value - (int)(value / 3);
                                        }
                                        battle.Deffence(value);
                                        break;
                                    case CardInfo.Type.POW:
                                        battle.Power(target, thisCard.skillValue[CardInfo.Type.POW]);
                                        break;
                                    case CardInfo.Type.WEAK:
                                        return false;
                                    case CardInfo.Type.EXTINCTION:
                                        return false;
                                }
                            }
                            break;
                        case CardInfo.CardType.POW:
                            CardInfo.Type[] pow_Temp = null;
                            if (thisCard.subType == CardInfo.Type.DEFAULT)
                            {
                                pow_Temp = new CardInfo.Type[1];
                                pow_Temp[0] = thisCard.type;
                            }
                            else
                            {
                                pow_Temp = new CardInfo.Type[2];
                                pow_Temp[0] = thisCard.type;
                                pow_Temp[1] = thisCard.subType;
                            }
                            battle.energy -= thisCard.cost;
                            for (int i = 0; i < pow_Temp.Length; i++)
                            {
                                switch (pow_Temp[i])
                                {
                                    case CardInfo.Type.DEFAULT:
                                        return false;
                                    case CardInfo.Type.ATK:
                                        return false;
                                    case CardInfo.Type.DEF:
                                        return false;
                                    case CardInfo.Type.POW:
                                        battle.Power(target, thisCard.skillValue[CardInfo.Type.POW]);
                                        break;
                                    case CardInfo.Type.WEAK:
                                        battle.Weak(target, thisCard.skillValue[CardInfo.Type.WEAK]);
                                        break;
                                    case CardInfo.Type.EXTINCTION:
                                        return false;
                                }
                            }
                            
                            break;
                        case CardInfo.CardType.ABNORMAL:
                            switch (thisCard.type)
                            {
                                case CardInfo.Type.DEFAULT:
                                    return false;
                                case CardInfo.Type.ATK:
                                    return false;
                                case CardInfo.Type.DEF:
                                    return false;
                                case CardInfo.Type.POW:
                                    return false;
                                case CardInfo.Type.WEAK:
                                    return false;
                                case CardInfo.Type.EXTINCTION:
                                    battle.energy -= thisCard.cost;
                                    battle.DeleteCardMove(gameObject);
                                    break;
                            }
                            break;
                    }
                    break;
                case "Monster":
                    switch (thisCard.cardType)
                    {
                        case CardInfo.CardType.DEFAULT:
                            return false;
                        case CardInfo.CardType.ATK:
                            battle.energy -= thisCard.cost;
                            battle.Attack(target, thisCard.skillValue[CardInfo.Type.ATK]);
                            break;
                        case CardInfo.CardType.SK:
                            CardInfo.Type[] sk_Temp = null;
                            if (thisCard.subType == CardInfo.Type.DEFAULT)
                            {
                                sk_Temp = new CardInfo.Type[1];
                                sk_Temp[0] = thisCard.type;
                            }
                            else
                            {
                                sk_Temp = new CardInfo.Type[2];
                                sk_Temp[0] = thisCard.type;
                                sk_Temp[1] = thisCard.subType;
                            }
                            battle.energy -= thisCard.cost;
                            for (int i = 0; i < sk_Temp.Length; i++)
                            {
                                switch (sk_Temp[i])
                                {
                                    case CardInfo.Type.DEFAULT:
                                        return false;
                                    case CardInfo.Type.ATK:
                                        return false;
                                    case CardInfo.Type.DEF:
                                        value = thisCard.skillValue[CardInfo.Type.DEF];
                                        if (battle.playerBufList[PlayerBuffType.IMPAIR] > 0)
                                        {
                                            value = value - (int)(value / 3);
                                        }
                                        battle.Deffence(value);
                                        break;
                                    case CardInfo.Type.POW:
                                        return false;
                                    case CardInfo.Type.WEAK:
                                        battle.Weak(target, thisCard.skillValue[CardInfo.Type.WEAK]);
                                        break;
                                    case CardInfo.Type.EXTINCTION:
                                        return false;
                                }
                            }
                            break;
                        case CardInfo.CardType.POW:
                            return false;
                        case CardInfo.CardType.ABNORMAL:
                            switch (thisCard.type)
                            {
                                case CardInfo.Type.DEFAULT:
                                    return false;
                                case CardInfo.Type.ATK:
                                    return false;
                                case CardInfo.Type.DEF:
                                    return false;
                                case CardInfo.Type.POW:
                                    return false;
                                case CardInfo.Type.WEAK:
                                    return false;
                                case CardInfo.Type.EXTINCTION:
                                    battle.energy -= thisCard.cost;
                                    battle.DeleteCardMove(gameObject);
                                    break;
                            }
                            break;
                    }
                    break;
            }
            #region LastVersion
            //for (int i = 0; i < thisCardValue.type.Count; i++)
            //{
            //    switch (thisCardValue.type[i])
            //    {
            //        case CardType.ATK:
            //            if (target.tag.Equals("Player"))
            //            {
            //                return false;
            //            }
            //            if (i == 0)
            //            {
            //                battle.energy -= thisCard.cost;
            //            }
            //            battle.Attack(target, thisCardValue.skillValue[thisCardValue.type[i]]);
            //            break;
            //        case CardType.DEF:
            //            if (target.tag.Equals("Monster"))
            //            {
            //                return false;
            //            }
            //            if (i == 0)
            //            {
            //                battle.energy -= thisCard.cost;
            //            }
            //            battle.Deffence(thisCardValue.skillValue[thisCardValue.type[i]]);
            //            break;
            //        case CardType.POW:
            //            if (target.tag.Equals("Monster"))
            //            {
            //                return false;
            //            }
            //            if (i == 0)
            //            {
            //                battle.energy -= thisCard.cost;
            //            }
            //            battle.Power(target, thisCardValue.skillValue[thisCardValue.type[i]]);
            //            break;
            //        case CardType.WEAK:
            //            if (target.tag.Equals("Player"))
            //            {
            //                return false;
            //            }
            //            if (i == 0)
            //            {
            //                battle.energy -= thisCard.cost;
            //            }
            //            battle.Weak(target, thisCardValue.skillValue[thisCardValue.type[i]]);
            //            break;
            //        case CardType.EXTINCTION:
            //            if (i == 0)
            //            {
            //                battle.energy -= thisCard.cost;
            //            }
            //            battle.DeleteCardMove(gameObject);
            //            return true;
            //    }

            //}
            #endregion
        }
        else
        {
            return false;
        }
        battle.UsedCardMove(gameObject);
        return true;
    }
    
}
