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
    List<GameObject> bufPreFabs;//0.�� 1.���
    [SerializeField]
    List<GameObject> bufUI;//0.�� 1.���
    [SerializeField]
    GameObject playerBufPannel;
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
    public TextMeshProUGUI energy_Tmp;
    // Start is called before the first frame update
    void OnEnable()
    {
        playerBufPannel = GameObject.Find("Player_Buf_Pannel");
        shieldObj = GameObject.Find("Player_Shield");
        bufUI = new List<GameObject>();
        for (int i = 0; i < bufPreFabs.Count; i++)
        {
            bufUI.Add(Instantiate(bufPreFabs[i], playerBufPannel.transform));
        }

        for(int i = 0;i< bufUI.Count; i++)
        {
            bufUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 0.ToString();
        }
        energy_Tmp.text = 0 + "/" + 0;
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
            for (int i = 0; i < deleteContent.transform.childCount; i++)
            {
                deleteContent.transform.GetChild(i).GetComponent<MousePoint>().enabled = false;
            }
        }
        
        energy_Tmp.text = battle.energy + "/" + battle.maxEnergy;
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
