using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "EditMonsterData")]
public class EditMonsterData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";
    public EditMonsterSkillData monsterSkillEditor;
    public List<MonsterStat> items = new List<MonsterStat>();
    [HideInInspector]
    public MonsterExcelDataLoader monsterDataLoader;
    public List<string> Names = new List<string>();

    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        MonsterStat temp;
        temp = new MonsterStat();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "NAME":
                    temp.name = list[i].value;
                    temp.img = Resources.Load<Sprite>("MonsterImg/" + list[i].value);
                    break;
                case "HP":
                    temp.maxHp = int.Parse(list[i].value);
                    temp.hp = int.Parse(list[i].value);
                    break;
                case "SHIELD":
                    temp.shield = int.Parse(list[i].value);
                    break;
            }
        }
        items.Add(temp);
    }

}

[CustomEditor(typeof(EditMonsterData))]
public class MonsterDataEditor : Editor
{
    EditMonsterData data;
    
    const string savedMonsterKey = "SavedMonster";
    void OnEnable()
    {
        data = (EditMonsterData)target;
        data.monsterDataLoader = GameObject.Find("ExcelData").GetComponent<MonsterExcelDataLoader>();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");

        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        data.items.Clear();
        foreach (string dataName in data.Names)
        {
            data.UpdateStats(ss.rows[dataName], dataName);
        }
        data.monsterDataLoader.monsterExelInfo = data.items;
        EditorUtility.SetDirty(target);
        SaveMonsterList();
        LoadMonsterList();
    }
    private void SaveMonsterList()
    {
        // 카드 리스트를 직렬화하여 문자열로 저장
        for(int i = 0; i < data.items.Count; i++)
        {
            for(int j = 0; j < data.monsterSkillEditor.items.Count; j++)
            {
                if(data.items[i].name == data.monsterSkillEditor.items[j].name)
                {
                    data.items[i].AddSkill(data.monsterSkillEditor.items[j]);
                }
            }
        }
        string json = SerializeMonsterList(data.items);

        // PlayerPrefs에 저장
        PlayerPrefs.SetString(savedMonsterKey, json);
        //Debug.Log("Save : " + json);
        // 변경사항을 바로 저장
        PlayerPrefs.Save();
    }

    private void LoadMonsterList()
    {
        // PlayerPrefs에서 JSON 문자열 불러오기
        string json = PlayerPrefs.GetString(savedMonsterKey);
        //Debug.Log("Load : " + json);
        if (!string.IsNullOrEmpty(json))
        {
            // JSON 문자열을 역직렬화하여 카드 리스트로 변환
            data.items = DeserializeMonsterList(json);

        }
    }
    #region 추가삭제기능 현제안씀
    //// 카드 리스트에 카드 추가 예제
    //public void AddToCardList(CardInfo cardInfo)
    //{
    //    // 중복 카드 체크 (인덱스로 판별)
    //    if (!data.items.Contains(cardInfo))
    //    {
    //        data.items.Add(cardInfo);

    //        // 변경사항을 저장
    //        SaveCardList();
    //    }
    //}

    //// 카드 리스트에서 카드 제거 예제
    //public void RemoveFromCardList(CardInfo cardInfo)
    //{
    //    if (data.items.Contains(cardInfo))
    //    {
    //        data.items.Remove(cardInfo);

    //        // 변경사항을 저장
    //        SaveCardList();
    //    }
    //}
    #endregion


    [System.Serializable]
    private class ListSerilizeObj
    {
        public List<MonsterInfoSerializable> serializableList = new List<MonsterInfoSerializable>();
    }

    // 카드 리스트를 직렬화하는 함수
    private string SerializeMonsterList(List<MonsterStat> monsterList)
    {
        ListSerilizeObj obj = new ListSerilizeObj();
        List<MonsterInfoSerializable> serializableList = obj.serializableList;// new List<CardInfoSerializable>();
        foreach (var monsterInfo in monsterList)
        {
            serializableList.Add(new MonsterInfoSerializable(monsterInfo));
        }
        string str = JsonUtility.ToJson(obj);
        Debug.Log($"Passing Json: {str}");
        return str;
    }

    // 직렬화된 문자열을 카드 리스트로 역직렬화하는 함수
    private List<MonsterStat> DeserializeMonsterList(string json)
    {
        ListSerilizeObj obj = JsonUtility.FromJson<ListSerilizeObj>(json);

        List<MonsterStat> monsterList = new List<MonsterStat>();

        foreach (var serializable in obj.serializableList)
        {
            monsterList.Add(serializable.ToMonsterInfo());
        }

        return monsterList;
    }
    [Serializable]
    private class MonsterInfoSerializable
    {
        public string name;
        public int maxHp;
        public int hp;
        public Sprite img;
        public int shield;
        public List<MonsterSkill> skillList;
        public List<SerializableKeyValuePair<SkillType, int>> skillValue = new List<SerializableKeyValuePair<SkillType, int>>();
        public MonsterInfoSerializable(MonsterStat monsterInfo)
        {
            this.name = monsterInfo.name;
            this.maxHp = monsterInfo.maxHp;
            this.hp = monsterInfo.hp;
            this.img = monsterInfo.img;
            this.shield = monsterInfo.shield;
            this.skillList = monsterInfo.skillList;
            foreach (var kvp in monsterInfo.skillList)
            {
                foreach(var tt in kvp.skillValue)
                {
                    skillValue.Add(new SerializableKeyValuePair<SkillType, int>(tt.Key, tt.Value));
                }
                //kvp.Key = cardInfo.skillValue
                
            }
        }

        public MonsterStat ToMonsterInfo()
        {
            MonsterStat monsterInfo = new MonsterStat
            {
                name = this.name,
                maxHp = this.maxHp,
                hp = this.hp,
                img = this.img,
                shield = this.shield,
                skillList = this.skillList
            };

            foreach (var kvp in monsterInfo.skillList)
            {
                foreach (var tt in kvp.skillValue)
                {
                    kvp.skillValue.Add(tt.Key, tt.Value);
                }
                monsterInfo.skillList.Add(kvp);
            }
            return monsterInfo;
        }
    }

    [Serializable]
    private class SerializableKeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}