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

    [SerializeField]
    GameObject beforeContent;
    [SerializeField]
    GameObject afterContent;
    [SerializeField]
    GameObject deleteContent;
    // Start is called before the first frame update
    void Start()
    {
        playerBufPannel = GameObject.Find("Player_Buf_Pannel");
        shieldObj = GameObject.Find("Player_Shield");
        monsterList = new List<GameObject>();
        bufUI = new List<GameObject>();
        for (int i = 0; i < bufPreFabs.Count; i++)
        {
            bufUI.Add(Instantiate(bufPreFabs[i], playerBufPannel.transform));
        }

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

        for(int i = 0; i <= (int)PlayerBuffType.CONSCIOUS; i++)
        {
            if (battle.playerBufList[(PlayerBuffType)i] > 0)
            {
                bufUI[i].SetActive(true);
                if (i == (int)PlayerBuffType.SLIMECARD)
                {
                    bufUI[i].SetActive(false);
                }
            }
            else
            {
                bufUI[i].SetActive(false);
            }
        }
        shieldObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.shiled.ToString();
        for(int i =0;i< battle.playerBufList.Count; i++)
        {
            bufUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.playerBufList[(PlayerBuffType)i].ToString();
        }
        beforeDeck.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.beforUse.Count.ToString();
        afterDeck.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.afterUse.Count.ToString();
        deleteDeck.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = battle.deletCard.Count.ToString();
        if (afterUsedCardView.activeSelf)
        {
            for(int i=0;i< afterContent.transform.childCount; i++)
            {
                afterContent.transform.GetChild(i).GetComponent<MousePoint>().enabled = false;
            }
        }
        if (beforCardView.activeSelf)
        {
            for (int i = 0; i < beforeContent.transform.childCount; i++)
            {
                beforeContent.transform.GetChild(i).GetComponent<MousePoint>().enabled = false;
            }
        }
        if (deleteCardView.activeSelf)
        {
            for (int i = 0; i < afterContent.transform.childCount; i++)
            {
                deleteContent.transform.GetChild(i).GetComponent<MousePoint>().enabled = false;
            }
        }
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
