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
    [SerializeField]
    GameObject beforeDeck;
    [SerializeField]
    GameObject afterDeck;
    [SerializeField]
    GameObject deleteDeck;
    [SerializeField]
    Button closeDeck;
    [SerializeField]
    GameObject deckList;
    [SerializeField]
    GameObject afterUsedCardView;
    [SerializeField]
    GameObject beforCardView;
    [SerializeField]
    GameObject deleteCardView;
    // Start is called before the first frame update
    void Start()
    {
        playerBufPannel = GameObject.Find("Player_Buf_Pannel");
        shieldObj = GameObject.Find("Player_Shield");
        monsterList = new List<GameObject>();
        bufUI = new List<GameObject>();
        bufUI.Add(Instantiate(bufPreFabs[0], playerBufPannel.transform));
        bufUI.Add(Instantiate(bufPreFabs[1], playerBufPannel.transform));
        for(int i = 0;i< bufUI.Count; i++)
        {
            bufUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 0.ToString();
        }
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
        for(int i =0;i< battle.playerBufList.Count; i++)
        {
            bufUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.playerBufList[(PlayerBuffType)i].ToString();
        }
        beforeDeck.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.beforUse.Count.ToString();
        afterDeck.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.afterUse.Count.ToString();
        deleteDeck.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.deletCard.Count.ToString();
        
    }
    public void OnClickUsedCardView()
    {
        afterUsedCardView.SetActive(true);
    }
    public void OnClickExitUsedCardView()
    {
        afterUsedCardView.SetActive(false);
    }
    public void OnClickBeforCardView()
    {
        beforCardView.SetActive(true);
    }
    public void OnClickExitBeforCardView()
    {
        beforCardView.SetActive(false);
    }
    public void OnClickDeleteCardView()
    {
        deleteCardView.SetActive(true);
    }
    public void OnClickExitDeleteCardView()
    {
        deleteCardView.SetActive(false);
    }

    
}
