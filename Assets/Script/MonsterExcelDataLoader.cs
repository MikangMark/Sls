using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class MonsterExcelDataLoader : MonoBehaviour
{
    public List<MonsterStat> monsterInfo;
    public TextAsset monsterText;
    public int lineSize, rowSize;
    private void Awake()
    {
        TextAsset data = monsterText;
        string currentText = monsterText.text.Substring(0, monsterText.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        // 데이터 파싱
        string[] rows = data.text.Split(new char[] { '\n' });
        for (int i = 1; i < lineSize; i++)//0번째는 열목록 텍스트
        {
            string[] row = line[i].Split('\t');
            MonsterStat temp = new MonsterStat();
            Texture2D[] monsterImgs = Resources.LoadAll<Texture2D>("MonsterImg");
            Sprite sprite = null;
            Debug.Log(row[4]);
            for (int j = 0; j < monsterImgs.Length; j++)
            {
                string x = monsterImgs[j].name + '\r';
                if (monsterImgs[j].name.Equals(row[4])|| x.Equals(row[4]))
                {
                    sprite = Sprite.Create(monsterImgs[j], new Rect(0, 0, monsterImgs[j].width, monsterImgs[j].height), Vector2.zero);
                    sprite.name = monsterImgs[j].name;
                }
            }
            temp.InputInfo(row[0], int.Parse(row[1]), int.Parse(row[2]), int.Parse(row[3]), sprite);
            monsterInfo.Add(temp);
        }
    }
    // Start is called before the first frame update
    
}
