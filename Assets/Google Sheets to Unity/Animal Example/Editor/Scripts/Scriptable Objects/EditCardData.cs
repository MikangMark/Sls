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
public class EditCardData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<CardInfo> items = new List<CardInfo>();
    [HideInInspector]
    public ExcelDataLoader dataLoader;
    public List<string> Names = new List<string>();

    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        CardInfo temp;
        temp = new CardInfo();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "INDEX":
                    temp.index = int.Parse(list[i].value);
                    break;
                case "COUNT":
                    temp.count = int.Parse(list[i].value);
                    break;
                case "RAND":
                    if(list[i].value == "TRUE")
                    {
                        temp.randomTarget = true;
                    }
                    else
                    {
                        temp.randomTarget = false;
                    }
                    break;
                case "COST":
                    temp.cost = int.Parse(list[i].value);
                    break;
                case "CARDTYPE":
                    System.Enum.TryParse(list[i].value, out temp.cardType);
                    break;
                case "TYPE":
                    System.Enum.TryParse(list[i].value, out temp.type);
                    break;
                case "TYPE_SUB":
                    if (list[i].value.Length > 0)
                    {
                        System.Enum.TryParse(list[i].value, out temp.subType);
                    }
                    break;
                case "VALUE":
                    temp.skillValue.Add(temp.type, int.Parse(list[i].value));
                    break;
                case "VALUE_SUB":
                    if (list[i].value.Length > 0)
                    {
                        temp.skillValue.Add(temp.subType, int.Parse(list[i].value));
                    }
                    break;
                case "NAME":
                    temp.title = list[i].value;
                    temp.cardImg = Resources.Load<Sprite>("CardImg/" + temp.title);
                    break;
                case "TEXT":
                    temp.text = list[i].value;
                    break;
                case "SHOP":
                    temp.shop = int.Parse(list[i].value);
                    break;
            }
        }
        items.Add(temp);
    }

}

[CustomEditor(typeof(EditCardData))]
public class DataEditor : Editor
{
    EditCardData data;
    public const string savedCardKey = "SavedCardKey";
    void OnEnable()
    {
        data = (EditCardData)target;
        data.dataLoader = GameObject.Find("ExcelData").GetComponent<ExcelDataLoader>();
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
        data.dataLoader.cardInfo = data.items;
        EditorUtility.SetDirty(target);
        SaveCardList();
        LoadCardList();
    }
    private void SaveCardList()
    {
        // 카드 리스트를 직렬화하여 문자열로 저장
        string json = SerializeCardList(data.items);

        // PlayerPrefs에 저장
        PlayerPrefs.SetString(savedCardKey, json);

        // 변경사항을 바로 저장
        PlayerPrefs.Save();
    }

    private void LoadCardList()
    {
        // PlayerPrefs에서 JSON 문자열 불러오기
        string json = PlayerPrefs.GetString(savedCardKey);

        if (!string.IsNullOrEmpty(json))
        {
            // JSON 문자열을 역직렬화하여 카드 리스트로 변환
            data.items = DeserializeCardList(json);

        }
    }

    // 카드 리스트에 카드 추가 예제
    public void AddToCardList(CardInfo cardInfo)
    {
        // 중복 카드 체크 (인덱스로 판별)
        if (!data.items.Contains(cardInfo))
        {
            data.items.Add(cardInfo);

            // 변경사항을 저장
            SaveCardList();
        }
    }

    // 카드 리스트에서 카드 제거 예제
    public void RemoveFromCardList(CardInfo cardInfo)
    {
        if (data.items.Contains(cardInfo))
        {
            data.items.Remove(cardInfo);

            // 변경사항을 저장
            SaveCardList();
        }
    }

    // 카드 리스트를 직렬화하는 함수
    private string SerializeCardList(List<CardInfo> cardList)
    {
        List<CardInfoSerializable> serializableList = new List<CardInfoSerializable>();
        foreach (var cardInfo in cardList)
        {
            serializableList.Add(new CardInfoSerializable(cardInfo));
        }

        return JsonUtility.ToJson(serializableList);
    }

    // 직렬화된 문자열을 카드 리스트로 역직렬화하는 함수
    private List<CardInfo> DeserializeCardList(string json)
    {
        List<CardInfoSerializable> serializableList = JsonUtility.FromJson<List<CardInfoSerializable>>(json);
        List<CardInfo> cardList = new List<CardInfo>();

        foreach (var serializable in serializableList)
        {
            cardList.Add(serializable.ToCardInfo());
        }

        return cardList;
    }
    [Serializable]
    private class CardInfoSerializable
    {
        public int index;
        public int cost;
        public int count;
        public string title;
        public CardInfo.CardType cardType;
        public CardInfo.Type type;
        public CardInfo.Type subType;
        public List<SerializableKeyValuePair<CardInfo.Type, int>> skillValue = new List<SerializableKeyValuePair<CardInfo.Type, int>>();
        public string text;
        public bool randomTarget;
        public int shop;

        public CardInfoSerializable(CardInfo cardInfo)
        {
            this.index = cardInfo.index;
            this.cost = cardInfo.cost;
            this.count = cardInfo.count;
            this.title = cardInfo.title;
            this.cardType = cardInfo.cardType;
            this.type = cardInfo.type;
            this.subType = cardInfo.subType;
            this.text = cardInfo.text;
            this.randomTarget = cardInfo.randomTarget;
            this.shop = cardInfo.shop;

            foreach (var kvp in cardInfo.skillValue)
            {
                skillValue.Add(new SerializableKeyValuePair<CardInfo.Type, int>(kvp.Key, kvp.Value));
            }
        }

        public CardInfo ToCardInfo()
        {
            CardInfo cardInfo = new CardInfo
            {
                index = this.index,
                cost = this.cost,
                count = this.count,
                title = this.title,
                cardType = this.cardType,
                type = this.type,
                subType = this.subType,
                text = this.text,
                randomTarget = this.randomTarget,
                shop = this.shop,
                skillValue = new Dictionary<CardInfo.Type, int>()
            };

            foreach (var kvp in skillValue)
            {
                cardInfo.skillValue.Add(kvp.Key, kvp.Value);
            }

            return cardInfo;
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
    #region 1차 플레이어프리펩
    /*
    public void SaveCardList()
    {
        Debug.Log("카드정보저장");
        string json = JsonUtility.ToJson(new SerializableList<CardInfo> { items = data.items });
        PlayerPrefs.SetString(savedCardKey, json);
        PlayerPrefs.Save();
        LoadCardList();
    }
    
    public void LoadCardList()
    {
        if (PlayerPrefs.HasKey(savedCardKey))
        {
            string json = PlayerPrefs.GetString(savedCardKey);
            SerializableList<CardInfo> serializableList = JsonUtility.FromJson<SerializableList<CardInfo>>(json);
            data.items = serializableList.items;
            for(int i = 0; i < data.items.Count; i++)
            {
                Debug.Log(data.items[i].title);
                Debug.Log(data.items[i].type.ToString());
                Debug.Log(data.items[i].skillValue[data.items[i].type]);
            }
        }
    }

    public void TestLoadCardInfo()
    {
        if (PlayerPrefs.HasKey(savedCardKey))
        {
            string json = PlayerPrefs.GetString(savedCardKey);
            SerializableList<CardInfo> serializableList = JsonUtility.FromJson<SerializableList<CardInfo>>(json);
            data.items = serializableList.items;
            for (int i = 0; i < data.items.Count; i++)
            {
                Debug.Log(data.items[i].title);
                
            }
        }
    }

    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> items = new List<T>();
    }
    */
    #endregion
}