using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MonsterSkill
{
    public int no;
    public string text;

    public void InputInfo(int _no,string _text)
    {
        no = _no;
        text = _text;
    }
}

public class MonsterSkillExcelDataLoader : MonoBehaviour
{
    public List<MonsterSkill> monsterSkillInfo;
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
        // µ•¿Ã≈Õ ∆ƒΩÃ
        string[] rows = data.text.Split(new char[] { '\n' });
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            MonsterSkill temp = new MonsterSkill();
            temp.InputInfo(int.Parse(row[0]), row[1]);
            monsterSkillInfo.Add(temp);
        }
    }
}
