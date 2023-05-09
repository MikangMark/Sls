using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType { DEFAULT = 0, ATK, DEF, POW, WEAK, VULNER, IMPAIR, SLIMECARD, RESTRAINT, CONSCIOUS }
[System.Serializable]
public class MonsterSkill
{
    public List<SkillType> type;
    public Dictionary<SkillType, int> skillValue;
    public MonsterSkill()
    {
        type = new List<SkillType>();
        skillValue = new Dictionary<SkillType, int>();
    }
}

public class MonsterSkillExcelDataLoader : MonoBehaviour
{
    public Dictionary<int, MonsterSkill> monsterSkillInfo;
    public TextAsset monsterSkillText;
    public int lineSize, rowSize;
    // Start is called before the first frame update
    private void Awake()
    {
        TextAsset data = monsterSkillText;
        string currentText = monsterSkillText.text.Substring(0, monsterSkillText.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        monsterSkillInfo = new Dictionary<int, MonsterSkill>();
        // 데이터 파싱
        string[] rows = data.text.Split(new char[] { '\n' });
        for (int i = 1; i < lineSize; i++)//0번째는 목록
        {
            string[] row = line[i].Split('\t');
            MonsterSkill temp = new MonsterSkill();
            for(int j = 0; j < row.Length; j++)
            {
                if (j == 0)
                {
                    monsterSkillInfo.Add(int.Parse(row[j]), null);
                }
                else
                {
                    if (!(row[j].Equals("") || row[j].Equals("\r")))
                    {
                        switch (j)
                        {
                            case (int)SkillType.ATK:
                                temp.type.Add(SkillType.ATK);
                                temp.skillValue.Add(SkillType.ATK, int.Parse(row[j]));
                                break;
                            case (int)SkillType.DEF:
                                temp.type.Add(SkillType.DEF);
                                temp.skillValue.Add(SkillType.DEF, int.Parse(row[j]));
                                break;
                            case (int)SkillType.POW:
                                temp.type.Add(SkillType.POW);
                                temp.skillValue.Add(SkillType.POW, int.Parse(row[j]));
                                break;
                            case (int)SkillType.WEAK:
                                temp.type.Add(SkillType.WEAK);
                                temp.skillValue.Add(SkillType.WEAK, int.Parse(row[j]));
                                break;
                            case (int)SkillType.VULNER:
                                temp.type.Add(SkillType.VULNER);
                                temp.skillValue.Add(SkillType.VULNER, int.Parse(row[j]));
                                break;
                            case (int)SkillType.IMPAIR:
                                temp.type.Add(SkillType.IMPAIR);
                                temp.skillValue.Add(SkillType.IMPAIR, int.Parse(row[j]));
                                break;
                            case (int)SkillType.SLIMECARD:
                                temp.type.Add(SkillType.SLIMECARD);
                                temp.skillValue.Add(SkillType.SLIMECARD, int.Parse(row[j]));
                                break;
                            case (int)SkillType.RESTRAINT:
                                temp.type.Add(SkillType.RESTRAINT);
                                temp.skillValue.Add(SkillType.RESTRAINT, int.Parse(row[j]));
                                break;
                            case (int)SkillType.CONSCIOUS:
                                temp.type.Add(SkillType.CONSCIOUS);
                                temp.skillValue.Add(SkillType.CONSCIOUS, int.Parse(row[j]));
                                break;
                        }
                    }
                }
                
            }
            monsterSkillInfo[int.Parse(row[0])] = temp;
        }
    }
}
