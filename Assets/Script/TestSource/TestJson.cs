using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {



        List<CardInfo> cardInfo = new List<CardInfo>();
        string testSave = "test";
        List<CardInfo> testInfo = new List<CardInfo>();
        CardInfo info = new CardInfo();
        info.title = "a";
        cardInfo.Add(info);

        info = new CardInfo();
        info.title = "b";
        cardInfo.Add(info);

        info = new CardInfo();
        info.title = "c";
        cardInfo.Add(info);

        info = new CardInfo();
        info.title = "d";
        cardInfo.Add(info);


        string json = JsonUtility.ToJson(new ExcelDataLoader.SerializableList<CardInfo> { items = cardInfo });
        PlayerPrefs.SetString(testSave, json);
        PlayerPrefs.Save();
        string jsons = PlayerPrefs.GetString(testSave);
        SerializableList<CardInfo> serializableList = JsonUtility.FromJson<SerializableList<CardInfo>>(jsons);
        testInfo = serializableList.items;
        Debug.Log(testInfo.Count);
    }
    [System.Serializable]
    private class SerializableList<T>
    {
        public List<T> items = new List<T>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
