using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType { DEFAULT = 0, ATK, DEF, POW, WEAK }

[System.Serializable]
public class CardValue
{
    public int cost;
    public List<CardType> type;
    public Dictionary<CardType, int> skillValue;
    public CardValue()
    {
        type = new List<CardType>();
        skillValue = new Dictionary<CardType, int>();
    }
}
public class CardValueExcelDataLoader : MonoBehaviour
{
    public List<CardValue> cardValueInfo;//ī������
    public Dictionary<CardInfo, CardValue> allInfoCard;
    public TextAsset cardValueText;
    public int lineSize, rowSize;
    public ExcelDataLoader excelData;

    private void Awake()
    {
        TextAsset data = cardValueText;
        string currentText = cardValueText.text.Substring(0, cardValueText.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        cardValueInfo = new List<CardValue>();
        allInfoCard = new Dictionary<CardInfo, CardValue>();
        // ������ �Ľ�
        string[] rows = data.text.Split(new char[] { '\n' });
        for (int i = 1; i < lineSize; i++)//0��°�� ���
        {
            string[] row = line[i].Split('\t');
            CardValue temp = new CardValue();
            for (int j = 0; j < row.Length; j++)
            {
                if (j == 0)
                {
                    temp.cost = int.Parse(row[j]);
                }
                else
                {
                    if (!(row[j].Equals("") || row[j].Equals("\r")))
                    {
                        switch (j)
                        {
                            case (int)CardType.ATK:
                                temp.type.Add(CardType.ATK);
                                temp.skillValue.Add(CardType.ATK, int.Parse(row[j]));
                                break;
                            case (int)SkillType.DEF:
                                temp.type.Add(CardType.DEF);
                                temp.skillValue.Add(CardType.DEF, int.Parse(row[j]));
                                break;
                            case (int)SkillType.POW:
                                temp.type.Add(CardType.POW);
                                temp.skillValue.Add(CardType.POW, int.Parse(row[j]));
                                break;
                            case (int)SkillType.WEAK:
                                temp.type.Add(CardType.WEAK);
                                temp.skillValue.Add(CardType.WEAK, int.Parse(row[j]));
                                break;
                        }
                    }
                }

            }
            cardValueInfo.Add(temp);
            allInfoCard.Add(excelData.cardInfo[i - 1], temp);
        }


    }
}
