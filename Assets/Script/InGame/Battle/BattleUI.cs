using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    private Battle battle;
    [SerializeField]
    private GameObject shieldObj;
    [SerializeField]
    private GameObject monsterIntentPos;
    [SerializeField]
    List<GameObject> bufPreFabs;//0.Èû 1.Ãë¾à
    [SerializeField]
    List<GameObject> bufUI;//0.Èû 1.Ãë¾à
    [SerializeField]
    GameObject playerBufPannel;
    [SerializeField]
    List<GameObject> monsterList;

    // Start is called before the first frame update
    void Start()
    {
        playerBufPannel = GameObject.Find("Player_Buf_Pannel");
        shieldObj = GameObject.Find("Player_Shield");
        monsterList = new List<GameObject>();
        bufUI = new List<GameObject>();
        bufUI.Add(Instantiate(bufPreFabs[0], playerBufPannel.transform));
        bufUI.Add(Instantiate(bufPreFabs[1], playerBufPannel.transform));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (battle.shiled > 0)
        {
            shieldObj.SetActive(true);
            
        }
        else
        {
            shieldObj.SetActive(false);
        }

        if (battle.playerBufList[PlayerBuffType.POW] > 0)
        {
            bufUI[0].SetActive(true);
        }
        else
        {
            bufUI[0].SetActive(false);
        }

        if (battle.playerBufList[PlayerBuffType.WEAK] > 0)
        {
            bufUI[1].SetActive(true);
        }
        else
        {
            bufUI[1].SetActive(false);
        }
        shieldObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.shiled.ToString();
    }
}
