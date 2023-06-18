using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using CharType = Character.CharType;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI playerName_Tmp;
    CharType charType;
    public TextMeshProUGUI charType_Tmp;
    public TextMeshProUGUI hp_Tmp;
    public TextMeshProUGUI money_Tmp;
    public TextMeshProUGUI deckCount_Tmp;
    public GameObject battle_Img;
    public GameObject map;
    public Battle battle;

    public GameObject battleField;
    public GameObject battleScript;
    public GameObject battleUI;

    public List<GameObject> battleControll;

    public GameObject mainBattleField;

    [SerializeField]
    GameObject cardPrf;
    [SerializeField]
    GameObject content;

    [SerializeField]
    GameObject deckList_View;

    [SerializeField]
    GameObject reward_View;

    [SerializeField]
    List<GameObject> reward_Btn;

    bool mapActive = true;

    bool OnDeckView = false;
    bool battleActive = false;
    bool same = false;
    private void Awake()
    {
        CreateDeckObj();
        battleControll = new List<GameObject>();
        battleControll.Add(battleField);
        battleControll.Add(battleScript);
        battleControll.Add(battleUI);
        BattleTurnOff();
    }
    private void Start()
    {
        playerName_Tmp.text = "플레이어이름";
        charType_Tmp.text = "캐릭터이름";
        hp_Tmp.text = 0 + "/" + 0;
        money_Tmp.text = 0.ToString();
        deckCount_Tmp.text = 0.ToString();
        //battle_Img.SetActive(false);
        //map.SetActive(false);
        
        
        deckList_View.SetActive(false);
    }
    private void FixedUpdate()
    {
        charType_Tmp.text = InGame.Instance.charInfo.charType.ToString();
        hp_Tmp.text = InGame.Instance.charInfo.hp + "/" + InGame.Instance.charInfo.maxHp;
        money_Tmp.text = InGame.Instance.charInfo.money.ToString();
        deckCount_Tmp.text = Deck.Instance.deck.Count.ToString();
    }
    private void Update()
    {
        deckList_View.SetActive(OnDeckView);
        InGame.Instance.openDeckView = OnDeckView;
        if (same != battle.thisActive)
        {
            BattleActive();
        }
        same = battle.thisActive;
    }
    void BattleActive()
    {
        if (battle.thisActive)
        {
            BattleTurnOn();
        }
        else
        {
            BattleTurnOff();
        }
    }
    public void BattleTurnOn()
    {
        mapActive = false;
        for (int i = 0; i < battleControll.Count; i++)
        {
            battleControll[i].SetActive(!mapActive);
        }
        map.SetActive(mapActive);
    }
    public void BattleTurnOff()
    {
        mapActive = true;
        for (int i = 0; i < battleControll.Count; i++)
        {
            battleControll[i].SetActive(!mapActive);
        }
        map.SetActive(mapActive);
    }
    public void CreateDeckObj()
    {
        GameObject temp;
        for (int i = 0; i < Deck.Instance.deck.Count; i++)
        {
            temp = Instantiate(cardPrf, content.transform);
            temp.GetComponent<OneCard>().thisCard = Deck.Instance.deck[i];
            temp.name = "Card[" + i + "]";
            for (int j = 0; j < temp.transform.childCount; j++)
            {
                switch (j)
                {
                    case 0:
                        temp.transform.GetChild(j).name = "CostImg" + i;
                        temp.transform.GetChild(j).GetChild(0).name = "CostText" + i;
                        break;
                    case 1:
                        temp.transform.GetChild(j).name = "CardTitle" + i;
                        break;
                    case 2:
                        temp.transform.GetChild(j).name = "CardText" + i;
                        break;
                    case 3:
                        temp.transform.GetChild(j).name = "CardImg" + i;
                        break;
                    case 4:
                        temp.transform.GetChild(j).name = "CardType" + i;
                        break;
                }
            }
            Deck.Instance.cardList_Obj.Add(temp);
        }
    }
    public void OnClickDeckView_Btn()
    {
        if (OnDeckView)
        {
            OnDeckView = false;
        }
        else
        {
            OnDeckView = true;
        }
        
    }

    public void OnClickMapView_Btn()
    {
        if (battle.thisActive)
        {
            if (mapActive)
            {
                mapActive = false;
            }
            else
            {
                mapActive = true;
            }
            map.SetActive(mapActive);
            mainBattleField.SetActive(!mapActive);
        }
    }

    public void TurnRewardView()
    {
        if (reward_View.activeSelf)
        {
            reward_View.SetActive(false);
        }
        else
        {
            reward_View.SetActive(true);
        }
    }

    public void SetRewardView()
    {
        for(int i = 0; i < reward_Btn.Count; i++)
        {
            reward_Btn[i].SetActive(false);
        }
        for(int i = 0; i < InGame.Instance.rewardCardGroup + 1; i++)
        {
            reward_Btn[i].SetActive(true);
            if (i == 0)
            {
                reward_Btn[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = InGame.Instance.rewardGold + "G";
                //reward_Btn[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("BossCardReward");
                reward_Btn[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gold");
            }
            else
            {

            }
        }
    }
}
