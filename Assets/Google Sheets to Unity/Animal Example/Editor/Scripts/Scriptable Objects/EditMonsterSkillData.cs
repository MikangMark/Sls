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
[CreateAssetMenu(fileName = "EditMonsterSkillData")]
public class EditMonsterSkillData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<MonsterSkill> items = new List<MonsterSkill>();
    [HideInInspector]
    public MonsterSkillExcelDataLoader monsterSkillDataLoader;
    public List<string> Names = new List<string>();
    public EditMonsterData editMonster;
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        MonsterSkill temp;
        temp = new MonsterSkill();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "INDEX":
                    temp.index = int.Parse(list[i].value);
                    break;
                case "NAME":
                    temp.name = list[i].value;
                    break;
                case "ATK":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.ATK);
                    temp.skillValue.Add(SkillType.ATK, int.Parse(list[i].value));
                    break;
                case "DEF":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.DEF);
                    temp.skillValue.Add(SkillType.DEF, int.Parse(list[i].value));
                    break;
                case "POW":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.POW);
                    temp.skillValue.Add(SkillType.POW, int.Parse(list[i].value));
                    break;
                case "WEAK":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.WEAK);
                    temp.skillValue.Add(SkillType.WEAK, int.Parse(list[i].value));
                    break;
                case "VULNER":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.VULNER);
                    temp.skillValue.Add(SkillType.VULNER, int.Parse(list[i].value));
                    break;
                case "IMPAIR":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.IMPAIR);
                    temp.skillValue.Add(SkillType.IMPAIR, int.Parse(list[i].value));
                    break;
                case "SLIMECARD":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.SLIMECARD);
                    temp.skillValue.Add(SkillType.SLIMECARD, int.Parse(list[i].value));
                    break;
                case "RESTRAINT":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.RESTRAINT);
                    temp.skillValue.Add(SkillType.RESTRAINT, int.Parse(list[i].value));
                    break;
                case "CONSCIOUS":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    temp.type.Add(SkillType.CONSCIOUS);
                    temp.skillValue.Add(SkillType.CONSCIOUS, int.Parse(list[i].value));
                    break;
            }
        }
        for(int i = 0; i < editMonster.items.Count; i++)
        {
            if (editMonster.items[i].name.Equals(temp.name))
            {
                editMonster.items[i].skillList.Add(temp);
            }
        }
        items.Add(temp);
    }

}

[CustomEditor(typeof(EditMonsterSkillData))]
public class MonsterSkillDataEditor : Editor
{
    EditMonsterSkillData data;
    public string savedMonsterSKillKey = "SavedMonsterSkill";
    public string monsterskill;
    void OnEnable()
    {
        data = (EditMonsterSkillData)target;
        data.monsterSkillDataLoader = GameObject.Find("ExcelData").GetComponent<MonsterSkillExcelDataLoader>();
        
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
        data.monsterSkillDataLoader.monsterSkillInfo = data.items;
        EditorUtility.SetDirty(target);
        SaveMonsterSkillList();
        LoadMonsterSkillList();
    }
    private void SaveMonsterSkillList()
    {
        // 카드 리스트를 직렬화하여 문자열로 저장
        string json = SerializeMonsterSkillList(data.items);

        // PlayerPrefs에 저장
        PlayerPrefs.SetString(savedMonsterSKillKey, json);
        //Debug.Log("Save : " + json);
        // 변경사항을 바로 저장
        PlayerPrefs.Save();
    }

    private void LoadMonsterSkillList()
    {
        // PlayerPrefs에서 JSON 문자열 불러오기
        string json = PlayerPrefs.GetString(savedMonsterSKillKey);
        //Debug.Log("Load : " + json);
        if (!string.IsNullOrEmpty(json))
        {
            // JSON 문자열을 역직렬화하여 카드 리스트로 변환
            data.items = DeserializeCardList(json);

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
        public List<MonsterSkillInfoSerializable> serializableList = new List<MonsterSkillInfoSerializable>();
    }

    // 카드 리스트를 직렬화하는 함수
    private string SerializeMonsterSkillList(List<MonsterSkill> cardList)
    {
        ListSerilizeObj obj = new ListSerilizeObj();
        List<MonsterSkillInfoSerializable> serializableList = obj.serializableList;// new List<CardInfoSerializable>();
        foreach (var cardInfo in cardList)
        {
            serializableList.Add(new MonsterSkillInfoSerializable(cardInfo));
        }
        string str = JsonUtility.ToJson(obj);
        Debug.Log($"Passing Json: {str}");
        return str;
    }

    // 직렬화된 문자열을 카드 리스트로 역직렬화하는 함수
    private List<MonsterSkill> DeserializeCardList(string json)
    {
        ListSerilizeObj obj = JsonUtility.FromJson<ListSerilizeObj>(json);

        List<MonsterSkill> monsterSkillList = new List<MonsterSkill>();

        foreach (var serializable in obj.serializableList)
        {
            monsterSkillList.Add(serializable.ToMonsterSkillInfo());
        }

        return monsterSkillList;
    }


    public class MonsterJson : MonsterSkill
    {
        public List<SerializableKeyValuePair<SkillType, int>> Dict2List = new List<SerializableKeyValuePair<SkillType, int>>();


        public MonsterJson(MonsterSkill monsterSkillInfo)
        {
            this.index = monsterSkillInfo.index;
            this.name = monsterSkillInfo.name;
            this.type = monsterSkillInfo.type;

            AdjustList();
        }

        public void AdjustList()
        {
            foreach (var kvp in skillValue)
            {
                Dict2List.Add(new SerializableKeyValuePair<SkillType, int>(kvp.Key, kvp.Value));
            }

        }
    }


    [Serializable]
    private class MonsterSkillInfoSerializable
    {
        public int index;
        public string name;
        public List<SkillType> type;
        public List<SerializableKeyValuePair<SkillType, int>> skillValue = new List<SerializableKeyValuePair<SkillType, int>>();

        public MonsterSkillInfoSerializable(MonsterSkill monsterSkillInfo)
        {
            this.index = monsterSkillInfo.index;
            this.name = monsterSkillInfo.name;
            this.type = monsterSkillInfo.type;


            foreach (var kvp in monsterSkillInfo.skillValue)
            {
                //kvp.Key = cardInfo.skillValue
                skillValue.Add(new SerializableKeyValuePair<SkillType, int>(kvp.Key, kvp.Value));
            }
        }

        public MonsterSkill ToMonsterSkillInfo()
        {
            MonsterSkill monsterSkillInfo = new MonsterSkill
            {
                index = this.index,
                name = this.name,
                type = this.type,
                skillValue = new Dictionary<SkillType, int>()
            };

            foreach (var kvp in skillValue)
            {
                monsterSkillInfo.skillValue.Add(kvp.Key, kvp.Value);
            }

            return monsterSkillInfo;
        }
    }

    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue>
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