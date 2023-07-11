using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CardInfo : IEquatable<CardInfo>
{
    public enum CardType { DEFAULT = 0, ATK, SK , POW, ABNORMAL }//POW �Ŀ�ī��
    public enum Type { DEFAULT = 0, ATK, DEF, POW, WEAK, EXTINCTION }//POW ������

    public int index;
    public int cost;
    public string title;
    public CardType cardType;
    public Type type;
    public Type subType;
    public Dictionary<Type, int> skillValue;
    public string text;
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
    public CardInfo(int _index,int _cost, string _title, CardType _type, Type _Type, Type _subType, Dictionary<Type, int> _skillValue, string _text, Sprite _img)
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
    }
    public void InputInfo(int _index, int _cost, string _title, CardType _type, Type _Type, Type _subType, Dictionary<Type, int> _skillValue, string _text, Sprite _img)
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
    public List<CardInfo> cardInfo;//��ü ī����
    public CardData cardData;
    private void Start()
    {
        cardInfo = cardData.items;
        
    }
        //public CardInfo SearchCardType(Type _)
        
}