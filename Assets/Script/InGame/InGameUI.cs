using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using CharType = Character.CharType;

public class InGameUI : MonoBehaviour
{
    Battle battle;
    public TextMeshProUGUI playerName_Tmp;
    CharType charType;
    public TextMeshProUGUI charType_Tmp;
    public TextMeshProUGUI hp_Tmp;
    public TextMeshProUGUI money_Tmp;
    public TextMeshProUGUI deckCount_Tmp;
    public GameObject battle_Img;
    public GameObject map;
    public TextMeshProUGUI energy_Tmp;

    [SerializeField]
    GameObject cardPrf;
    [SerializeField]
    GameObject content;

    [SerializeField]
    GameObject deckList_View;

    bool OnDeckView = false;
    private void Awake()
    {
        CreateDeckObj();   
    }
    private void Start()
    {
        battle = GameObject.Find("BattleScript").GetComponent<Battle>();
        playerName_Tmp.text = "플레이어이름";
        charType_Tmp.text = "캐릭터이름";
        hp_Tmp.text = 0 + "/" + 0;
        money_Tmp.text = 0.ToString();
        deckCount_Tmp.text = 0.ToString();
        //battle_Img.SetActive(false);
        //map.SetActive(false);
        energy_Tmp.text = 0 + "/" + 0;
        
        deckList_View.SetActive(false);
    }
    private void FixedUpdate()
    {
        charType_Tmp.text = InGame.Instance.charInfo.charType.ToString();
        hp_Tmp.text = InGame.Instance.charInfo.hp + "/" + InGame.Instance.charInfo.maxHp;
        money_Tmp.text = InGame.Instance.charInfo.money.ToString();
        deckCount_Tmp.text = Deck.Instance.deck.Count.ToString();
        energy_Tmp.text = battle.energy + "/" + battle.maxEnergy;

        
    }
    private void Update()
    {
        deckList_View.SetActive(OnDeckView);
        InGame.Instance.openDeckView = OnDeckView;
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
}
