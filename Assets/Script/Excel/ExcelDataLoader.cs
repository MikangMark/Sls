using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CardInfo : IEquatable<CardInfo>
{
    public enum CardType { DEFAULT = 0, ATK, SK , POW, ABNORMAL, MAX }//POW 파워카드
    public enum Type { DEFAULT = 0, ATK, DEF, POW, WEAK, EXTINCTION }//POW 힘버프

    public int index;
    public int cost;
    public int count;
    public string title;
    public CardType cardType;
    public Type type;
    public Type subType;
    public Dictionary<Type, int> skillValue;
    public string text;
    public bool randomTarget;
    public Sprite cardImg;
    public CardInfo()
    {
        index = 0;
        cost = 0;
        title = "";
        cardType = CardType.DEFAULT;
        type = Type.DEFAULT;
        subType = Type.DEFAULT;
        skillValue = new Dictionary<Type, int>();
        text = "";
        cardImg = null;
    }
    public CardInfo(int _index,int _cost, int _count, string _title, CardType _type, Type _Type, Type _subType, Dictionary<Type, int> _skillValue, string _text,bool _randomTarget, Sprite _img)
    {
        index = _index;
        cost = _cost;
        title = _title;
        cardType = _type;
        type = _Type;
        subType = _subType;
        skillValue = _skillValue;
        text = _text;
        cardImg = _img;
        count = _count;
        randomTarget = _randomTarget;
    }
    public void InputInfo(int _index, int _cost, string _title, int _count, CardType _type, Type _Type, Type _subType, Dictionary<Type, int> _skillValue, string _text, bool _randomTarget, Sprite _img)
    {
        index = _index;
        cost = _cost;
        title = _title;
        cardType = _type;
        type = _Type;
        subType = _subType;
        skillValue = _skillValue;
        text = _text;
        cardImg = _img;
        count = _count;
        randomTarget = _randomTarget;
    }
    
    

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + cost.GetHashCode();
        hash = hash * 23 + title.GetHashCode();
        hash = hash * 23 + cardType.GetHashCode();
        hash = hash * 23 + text.GetHashCode();
        hash = hash * 23 + cardImg.GetHashCode();
        return hash;
    }

    public override bool Equals(object obj)
    {
        if (obj is CardInfo other)
        {
            return Equals(other);
        }
        return false;
    }

    public bool Equals(CardInfo other)
    {
        return Mathf.Approximately(cost, other.cost) && string.Equals(title, other.title) && Mathf.Approximately((int)cardType, (int)other.cardType) && string.Equals(text, other.text) && cardImg == other.cardImg;
    }
}

public class ExcelDataLoader : MonoBehaviour
{
    public List<CardInfo> cardInfo;//전체 카드목록
    public CardData cardData;

    public void InitSettingCardDatas()
    {
        if (cardData.items.Count != 0)
        {
            cardInfo = cardData.items;
        }
        
    }
        
}
