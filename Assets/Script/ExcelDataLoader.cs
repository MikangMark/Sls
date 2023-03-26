using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CardInfo
{
    public enum Type { DEFAULT = 0, ATK, SK }

    public int cost;
    public string title;
    public Type type;
    public string text;

    public void InputInfo(int _cost,string _title, Type _type, string _text)
    {
        cost = _cost;
        title = _title;
        type = _type;
        text = _text;
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
            temp.InputInfo(int.Parse(row[0]), row[1], tType, row[3]);
            cardInfo.Add(temp);
        }
    }
}
