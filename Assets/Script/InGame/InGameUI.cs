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

    private void Start()
    {
        playerName_Tmp.text = "플레이어이름";
        charType_Tmp.text = "캐릭터이름";
        hp_Tmp.text = 0 + "/" + 0;
        money_Tmp.text = 0.ToString();
    }
}
