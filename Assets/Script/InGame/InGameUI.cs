using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    }
    private void FixedUpdate()
    {
        charType_Tmp.text = InGame.Instance.charInfo.charType.ToString();
        hp_Tmp.text = InGame.Instance.charInfo.hp + "/" + InGame.Instance.charInfo.maxHp;
        money_Tmp.text = InGame.Instance.charInfo.money.ToString();
        deckCount_Tmp.text = Deck.Instance.deck.Count.ToString();
        energy_Tmp.text = battle.energy + "/" + battle.maxEnergy;
    }
}
