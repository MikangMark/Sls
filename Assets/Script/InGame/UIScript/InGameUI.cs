using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
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

    public GameObject clearCircle;

    [SerializeField]
    GameObject shopBtn;
    [SerializeField]
    GameObject exitShopBtn;
    [SerializeField]
    GameObject exitShopListBtn;

    [SerializeField]
    GameObject cardPrf;
    [SerializeField]
    GameObject content;

    [SerializeField]
    GameObject deckList_View;

    [SerializeField]
    GameObject reward_View;

    [SerializeField]
    GameObject lose_View;

    public List<GameObject> reward_Btn;

    [SerializeField]
    GameObject rewardCard_Parent;

    [SerializeField]
    List<GameObject> rewardCards;

    public GameObject selectedReward;

    [SerializeField]
    GameObject allRewardParent;

    [SerializeField]
    GameObject rewardOutBtn;

    [SerializeField]
    GameObject shop;
    [SerializeField]
    GameObject shopList;
    [SerializeField]
    GameObject disCardView;

    [SerializeField]
    GameObject rest;
    [SerializeField]
    UnknownManager unknownManager;
    [SerializeField]
    GameObject treasureView;

    public bool is_Reward = false;

    bool mapActive = true;

    bool OnDeckView = false;
    bool same = false;
    private void Awake()
    {
        
        battleControll = new List<GameObject>();
        battleControll.Add(battleField);
        battleControll.Add(battleScript);
        battleControll.Add(battleUI);
        BattleTurnOff();
    }
    private void Start()
    {
        //CreateDeckObj();
        playerName_Tmp.text = "플레이어이름";
        charType_Tmp.text = "캐릭터이름";
        hp_Tmp.text = 0 + "/" + 0;
        money_Tmp.text = 0.ToString();
        deckCount_Tmp.text = 0.ToString();
        lose_View.SetActive(false);
        reward_View.SetActive(false);
        deckList_View.SetActive(false);
        rewardCard_Parent.SetActive(false);
        rewardOutBtn.SetActive(false);
        shop.SetActive(false);
        rest.SetActive(false);
        treasureView.SetActive(false);
        InGame.Instance.relicInfoUi.SetActive(false);
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

        if (CheckGetAllReward())
        {
            rewardOutBtn.SetActive(true);
        }
    }
    void BattleActive()
    {
        if (battle.thisActive)
        {
            BattleTurnOn();
        }
        else
        {
            if(battle.result == Battle.BattleResult.Win)
            {
                TurnRewardView();
                InGame.Instance.SetReward();
            }
            else
            {
                Debug.Log("Lose");
                lose_View.SetActive(true);
            }
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
    public void TreasureEnter()
    {
        treasureView.SetActive(true);
    }
    public void ShopEnter()
    {
        shop.SetActive(true);
        shopBtn.SetActive(true);
        exitShopBtn.SetActive(false);
        exitShopListBtn.SetActive(false);
        shopList.SetActive(false);
    }
    public void UnkownEnter()
    {
        unknownManager.ActiveSelectUnknown(Random.Range(0, 4));
    }

    public void RestEnter()
    {
        rest.SetActive(true);
    }
    public void OnClickEnterShopBtn()
    {
        shopList.SetActive(true);
        shopBtn.SetActive(false);
        exitShopListBtn.SetActive(true);
        exitShopBtn.SetActive(false);
    }
    public void OnClickExitShopListBtn()
    {
        exitShopListBtn.SetActive(false);
        shopList.SetActive(false);
        shopBtn.SetActive(true);
        exitShopBtn.SetActive(true);
    }
    public void OnClickExitShopBtn()
    {
        InGame.Instance.currentFloor++;
        shop.SetActive(false);
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
            SetRewardView();
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
                reward_Btn[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gold");
                reward_Btn[i].GetComponent<Button>().onClick.AddListener(() => reward_Btn[i].GetComponent<RewardBtn>().OnClickGetReward_Gold());
                reward_Btn[i].tag = "RewardGold";
                reward_Btn[i].name = reward_Btn[i].tag;
            }
            else
            {
                reward_Btn[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "덱에 카드를 추가";
                reward_Btn[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("BossCardReward");
                reward_Btn[i].GetComponent<Button>().onClick.AddListener(reward_Btn[i].GetComponent<RewardBtn>().OnClickGetReward_Cards);
            }
        }
    }
    public void OnClickCardReward()
    {
        rewardCard_Parent.SetActive(true);
    }

    public void OnClickMoveMainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void SetRewardCards(int group_index)
    {
        for (int i = 0; i < 3; i++)
        {
            rewardCard_Parent.transform.GetChild(i).GetComponent<OneCard>().SetCard(InGame.Instance.rewardCards[group_index][i]);
            Debug.Log(InGame.Instance.rewardCards[group_index][i].index);
        }
    }

    bool CheckGetAllReward()
    {
        for(int i=0;i< allRewardParent.transform.childCount; i++)
        {
            if (allRewardParent.transform.GetChild(i).gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void OnClickRewardOutBtn()
    {
        TurnRewardView();
    }

    public void OnClickExitDisCardView()
    {
        disCardView.SetActive(false);
    }
}
