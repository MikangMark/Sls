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
[System.Serializable]
public class MonsterStat
{
    public string name;
    public int hp;
    public int atk;
    public int def;
    public Sprite img;

    public void InputInfo(string _name, int _hp, int _atk, int _def, Sprite _img)
    {
        name = _name;
        hp = _hp;
        atk = _atk;
        def = _def;
        img = _img;
    }
}

public class MonsterSkill_Info
{
    Dictionary<string, List<Dictionary<int, string>>> skillInfo;
    //  MonsterSkill
    //  슬라임	
    //  10001	10atk
    //  10002	5atk,8def
    //  10003	15def
    //  10004	1dbuf

}




public class ExcelDataLoader : MonoBehaviour
{
    public List<CardInfo> cardInfo;//전체 카드목록
    public List<TextAsset> txt;
    public List<MonsterStat> monsterStats;

    public int lineSize, rowSize;

    private void Awake()
    {
        for(int f = 0; f < txt.Count; f++)
        {
            TextAsset data = txt[f];
            string currentText = txt[f].text.Substring(0, txt[f].text.Length - 1);
            string[] line = currentText.Split('\n');
            lineSize = line.Length;
            rowSize = line[0].Split('\t').Length;
            // 데이터 파싱
            string[] rows = data.text.Split(new char[] { '\n' });
            
            for (int i = 0; i < lineSize; i++)
            {
                string[] row = line[i].Split('\t');
                CardInfo.Type tType = CardInfo.Type.DEFAULT;

                switch (row[0])
                {
                    #region CardInfoDataLoader
                    case "CardInfo":
                        if (i == 0)
                        {
                            continue;
                        }
                        else if (i == 1)
                        {
                            switch (row[0])
                            {
                                case "IronClead":
                                    break;
                                case "Silence":
                                    break;
                                case "Defact":
                                    break;
                                case "Wacher":
                                    break;
                            }

                        }
                        else
                        {
                            switch (row[3])
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
                            Sprite sprite = null;
                            for (int j = 0; j < cardImgs.Length; j++)
                            {
                                if (cardImgs[j].name.Equals(row[2]))
                                {
                                    //
                                    sprite = Sprite.Create(cardImgs[j], new Rect(0, 0, cardImgs[j].width, cardImgs[j].height), Vector2.zero);
                                    sprite.name = cardImgs[j].name;
                                }
                            }
                            temp.InputInfo(int.Parse(row[1]), row[2], tType, row[4], sprite);
                            cardInfo.Add(temp);
                            
                        }
                        break;
                    #endregion
                    case "Monster":
                        if (i == 0)
                        {
                            continue;
                        }
                        MonsterStat statTemp = new MonsterStat();
                        Texture2D[] monsterImgs = Resources.LoadAll<Texture2D>("CardImg");
                        Sprite monsterSprite = null;
                        for (int j = 0; j < monsterImgs.Length; j++)
                        {
                            if (monsterImgs[j].name.Equals(row[4]))
                            {
                                //
                                monsterSprite = Sprite.Create(monsterImgs[j], new Rect(0, 0, monsterImgs[j].width, monsterImgs[j].height), Vector2.zero);
                                monsterSprite.name = monsterImgs[j].name;
                            }
                        }
                        statTemp.InputInfo(row[0],int.Parse(row[1]), int.Parse(row[2]), int.Parse(row[3]), monsterSprite);
                        monsterStats.Add(statTemp);
                        break;
                    case "MonsterSkill":
                        if (i == 0)
                        {
                            continue;
                        }
                        break;
                }
                
            }
        }
    }
        // 엑셀 데이터 파일 로드
        
}
