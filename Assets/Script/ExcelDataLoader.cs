using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CardInfo : IEquatable<CardInfo>
{
    public enum Type { DEFAULT = 0, ATK, SK , POW, ABNORMAL }

    public int cost;
    public string title;
    public Type type;
    public string text;
    public Sprite cardImg;
    public CardInfo()
    {
        cost = 0;
        title = "";
        type = Type.DEFAULT;
        text = "";
        cardImg = null;
    }
    public CardInfo(int _cost, string _title, Type _type, string _text, Sprite _img)
    {
        cost = _cost;
        title = _title;
        type = _type;
        text = _text;
        cardImg = _img;
    }
    public void InputInfo(int _cost,string _title, Type _type, string _text, Sprite _img)
    {
        cost = _cost;
        title = _title;
        type = _type;
        text = _text;
        cardImg = _img;
    }
    
    

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + cost.GetHashCode();
        hash = hash * 23 + title.GetHashCode();
        hash = hash * 23 + type.GetHashCode();
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
        return Mathf.Approximately(cost, other.cost) && string.Equals(title, other.title) && Mathf.Approximately((int)type, (int)other.type) && string.Equals(text, other.text) && cardImg == other.cardImg;
    }
}

public class ExcelDataLoader : MonoBehaviour
{
    public List<CardInfo> cardInfo;//전체 카드목록
    public List<TextAsset> cardText;
    public int lineSize, rowSize;

    private void Awake()
    {
        for(int f = 0; f < cardText.Count; f++)//파일 단위
        {
            TextAsset data = cardText[f];
            string currentText = cardText[f].text.Substring(0, cardText[f].text.Length - 1);
            string[] line = currentText.Split('\n');
            lineSize = line.Length;
            rowSize = line[0].Split('\t').Length;
            // 데이터 파싱
            string[] rows = data.text.Split(new char[] { '\n' });
            
            for (int i = 0; i < lineSize; i++)
            {
                string[] row = line[i].Split('\t');
                CardInfo.Type tType = CardInfo.Type.DEFAULT;
                switch (row[2])
                {
                    case "ATK":
                        tType = CardInfo.Type.ATK;
                        break;
                    case "SK":
                        tType = CardInfo.Type.SK;
                        break;
                    case "POW":
                        tType = CardInfo.Type.POW;
                        break;
                    case "ABNORMAL":
                        tType = CardInfo.Type.ABNORMAL;
                        break;
                }
                

                CardInfo temp = new CardInfo();
                Texture2D[] cardImgs = Resources.LoadAll<Texture2D>("CardImg");
                Sprite sprite = null;
                for (int j = 0; j < cardImgs.Length; j++)
                {
                    if (cardImgs[j].name.Equals(row[1]))
                    {
                        sprite = Sprite.Create(cardImgs[j], new Rect(0, 0, cardImgs[j].width, cardImgs[j].height), Vector2.zero);
                        sprite.name = cardImgs[j].name;
                    }
                }
                temp.InputInfo(int.Parse(row[0]), row[1], tType, row[3], sprite);
                cardInfo.Add(temp);
            }
        }
    }
        //public CardInfo SearchCardType(Type _)
        
}
