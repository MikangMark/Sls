using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterUI : MonoBehaviour//모든 몬스터의 UI컨트롤
{
    public List<GameObject> monsterObj;
    public GameObject monsterPos;

    [SerializeField]
    GameObject pow_fab;
    [SerializeField]
    GameObject weak_fab;

    // Start is called before the first frame update
    void Start()
    {
        monsterObj = new List<GameObject>();
        for (int i=0; i < monsterPos.transform.childCount; i++)
        {
            monsterObj.Add(monsterPos.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < monsterPos.transform.childCount; i++)
        {
            UpdateMonster_UI(i);
        }
    }
    void UpdateMonster_UI(int index)
    {
        Monster monsterStat = monsterObj[index].GetComponent<Monster>();
        if (monsterStat.shiled > 0)
        {
            monsterObj[index].transform.GetChild(2).gameObject.SetActive(true);
            monsterObj[index].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = monsterStat.shiled.ToString();
        }
        else
        {
            monsterObj[index].transform.GetChild(2).gameObject.SetActive(false);
            monsterObj[index].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
        }
        if (monsterStat.bufList[MonsterBuffType.POW] > 0)
        {
            monsterObj[index].transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            monsterObj[index].transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = monsterStat.bufList[MonsterBuffType.POW].ToString();
        }
        else
        {
            monsterObj[index].transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            monsterObj[index].transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
        }
        if(monsterStat.bufList[MonsterBuffType.WEAK] > 0)
        {
            monsterObj[index].transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            monsterObj[index].transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = monsterStat.bufList[MonsterBuffType.WEAK].ToString();
        }
        else
        {
            monsterObj[index].transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            monsterObj[index].transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
        }
        
    }
}
