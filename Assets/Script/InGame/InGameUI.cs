using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    private void Start()
    {
        playerName_Tmp.text = "�÷��̾��̸�";
        charType_Tmp.text = "ĳ�����̸�";
        hp_Tmp.text = 0 + "/" + 0;
        money_Tmp.text = 0.ToString();
        deckCount_Tmp.text = 0.ToString();
        battle_Img.SetActive(false);
    }
    private void FixedUpdate()
    {
        charType_Tmp.text = InGame.Instance.charInfo.charType.ToString();
        hp_Tmp.text = InGame.Instance.charInfo.hp + "/" + InGame.Instance.charInfo.maxHp;
        money_Tmp.text = InGame.Instance.charInfo.money.ToString();
        deckCount_Tmp.text = Deck.Instance.deck.Count.ToString();
    }
}
