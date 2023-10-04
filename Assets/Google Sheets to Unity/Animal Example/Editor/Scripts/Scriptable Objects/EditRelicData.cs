using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "EditRelicData")]
public class EditRelicData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";
    public List<Relic> items = new List<Relic>();
    [HideInInspector]
    public RelicExcelDataLoader relicDataLoader;
    public List<string> Names = new List<string>();
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        Relic temp;
        temp = new Relic();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "Index":
                    temp.index = int.Parse(list[i].value);
                    break;
                case "Title":
                    temp.title = list[i].value;
                    temp.img = Resources.Load<Sprite>("RelicImg/" + temp.title);
                    break;
                case "Target":
                    System.Enum.TryParse(list[i].value, out temp.target);
                    break;
                case "Tag":
                    System.Enum.TryParse(list[i].value, out temp.relicTag);
                    break;
                case "Value":
                    temp.value = float.Parse(list[i].value);
                    break;
                case "Text":
                    temp.text = list[i].value;
                    break;
                case "InfoText":
                    temp.infoText = list[i].value;
                    break;

            }
        }
        items.Add(temp);
    }
}


[CustomEditor(typeof(EditRelicData))]
public class RelicDataEditor : Editor
{
    EditRelicData data;
    const string savedRelicKey = "SavedRelic";
    void OnEnable()
    {
        data = (EditRelicData)target;
        data.relicDataLoader = GameObject.Find("ExcelData").GetComponent<RelicExcelDataLoader>();
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
        data.relicDataLoader.relicInfo = data.items;
        EditorUtility.SetDirty(target);
        SaveRelicList();
        LoadRelicList();
    }
    private void SaveRelicList()
    {
        // ī�� ����Ʈ�� ����ȭ�Ͽ� ���ڿ��� ����
        string json = SerializeRelicList(data.items);

        // PlayerPrefs�� ����
        PlayerPrefs.SetString(savedRelicKey, json);
        //Debug.Log("Save : " + json);
        // ��������� �ٷ� ����
        PlayerPrefs.Save();
    }

    private void LoadRelicList()
    {
        // PlayerPrefs���� JSON ���ڿ� �ҷ�����
        string json = PlayerPrefs.GetString(savedRelicKey);
        //Debug.Log("Load : " + json);
        if (!string.IsNullOrEmpty(json))
        {
            // JSON ���ڿ��� ������ȭ�Ͽ� ī�� ����Ʈ�� ��ȯ
            data.items = DeserializeRelicList(json);

        }
    }

    [System.Serializable]
    private class ListSerilizeObj
    {
        public List<RelicInfoSerializable> serializableList = new List<RelicInfoSerializable>();
    }

    // ī�� ����Ʈ�� ����ȭ�ϴ� �Լ�
    private string SerializeRelicList(List<Relic> relicList)
    {
        ListSerilizeObj obj = new ListSerilizeObj();
        List<RelicInfoSerializable> serializableList = obj.serializableList;// new List<CardInfoSerializable>();
        foreach (var relicInfo in relicList)
        {
            serializableList.Add(new RelicInfoSerializable(relicInfo));
        }
        string str = JsonUtility.ToJson(obj);
        Debug.Log($"Passing Json: {str}");
        return str;
    }

    // ����ȭ�� ���ڿ��� ī�� ����Ʈ�� ������ȭ�ϴ� �Լ�
    private List<Relic> DeserializeRelicList(string json)//string������ ��ų��������
    {
        ListSerilizeObj obj = JsonUtility.FromJson<ListSerilizeObj>(json);

        List<Relic> relicList = new List<Relic>();

        foreach (var serializable in obj.serializableList)
        {
            relicList.Add(serializable.ToRelicInfo());
        }

        return relicList;
    }
    [Serializable]
    public class RelicInfoSerializable
    {
        public int index;
        public string title;
        public Relic.TARGET target;
        public Relic.TAG relicTag;
        public float value;
        public Sprite img;
        public string text;
        public string infoText;
        public RelicInfoSerializable(Relic relicInfo)
        {
            this.index = relicInfo.index;
            this.title = relicInfo.title;
            this.target = relicInfo.target;
            this.relicTag = relicInfo.relicTag;
            this.value = relicInfo.value;
            this.img = relicInfo.img;
            this.text = relicInfo.text;
            this.infoText = relicInfo.infoText;
        }
        public Relic ToRelicInfo()
        {
            Relic relicInfo = new Relic
            {
                index = this.index,
                title = this.title,
                target = this.target,
                relicTag = this.relicTag,
                value = this.value,
                img = this.img,
                text = this.text,
                infoText = this.infoText
            };
            return relicInfo;
        }
    }
}