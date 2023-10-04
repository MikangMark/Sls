using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Relic
{
    public enum TARGET { Player, Monster }
    public enum TAG { Mana, Hp, Buf, Def, Gold, Shop, Draw }
    public int index;
    public string title;
    public TARGET target;
    public TAG relicTag;
    public float value;
    public string text;
    public string infoText;
    public Sprite img;
}
public class RelicExcelDataLoader : MonoBehaviour
{
    public List<Relic> relicInfo;
    public RelicData relicData;

    public string savedRelicKey = "SavedRelic";

    private void Awake()
    {
        relicInfo = relicData.items;
    }
    public void InitSettingRelicDatas()
    {
        relicInfo = relicData.items;
    }

    public void SaveRelicList()
    {
        string json = JsonUtility.ToJson(new SerializableList<Relic> { items = relicInfo });
        PlayerPrefs.SetString(savedRelicKey, json);
        PlayerPrefs.Save();
    }

    public void LoadRelicList()
    {
        if (PlayerPrefs.HasKey(savedRelicKey))
        {
            string json = PlayerPrefs.GetString(savedRelicKey);
            SerializableList<Relic> serializableList = JsonUtility.FromJson<SerializableList<Relic>>(json);
            relicInfo = serializableList.items;
        }
    }

    [System.Serializable]
    private class SerializableList<T>
    {
        public List<T> items = new List<T>();
    }

}
