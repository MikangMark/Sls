using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CardInfo
{
    public enum Type { DEFAULT = 0, ATK, SK }

    public int cost;
    public string title;
    public Type type;
    public string text;
    public Sprite cardImg;

    public void InputInfo(int _cost,string _title, Type _type, string _text, Sprite _img)
    {
        cost = _cost;
        title = _title;
        type = _type;
        text = _text;
        cardImg = _img;
    }
}


public class ExcelDataLoader : MonoBehaviour
{
    public List<CardInfo> cardInfo;//전체 카드목록
    public TextAsset txt;

    public int lineSize, rowSize;

    private void Start()
    {
        // 엑셀 데이터 파일 로드
        TextAsset data = txt;

        string currentText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        // 데이터 파싱
        string[] rows = data.text.Split(new char[] { '\n' });

        for(int i = 0; i < lineSize; i++)
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
            }

            CardInfo temp = new CardInfo();
            Texture2D[] cardImgs = Resources.LoadAll<Texture2D>("CardImg");
            Debug.Log(cardImgs[0].name);
            Sprite sprite = null;
            for (int j = 0; j < cardImgs.Length; j++)
            {
                if (cardImgs[j].name.Equals(row[1]))
                {
                    //
                    sprite = Sprite.Create(cardImgs[j], new Rect(0, 0, cardImgs[j].width, cardImgs[j].height), Vector2.zero);
                    sprite.name = cardImgs[j].name;
                }
            }
            temp.InputInfo(int.Parse(row[0]), row[1], tType, row[3], sprite);
            cardInfo.Add(temp);
        }
    }
}
