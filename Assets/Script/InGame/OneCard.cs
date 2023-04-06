using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneCard : MonoBehaviour
{
    public static int value = 0;
    public CardInfo thisCard;
    public CardValue thisCardValue;
    public TextMeshProUGUI cCost;
    public TextMeshProUGUI cTitle;
    public TextMeshProUGUI cType;
    public TextMeshProUGUI cText;
    public Image cImg;
    private void Start()
    {
        cCost = GameObject.Find("CostText" + value).GetComponent<TextMeshProUGUI>();
        cTitle = GameObject.Find("CardTitle" + value).GetComponent<TextMeshProUGUI>();
        cType = GameObject.Find("CardType" + value).GetComponent<TextMeshProUGUI>();
        cText = GameObject.Find("CardText" + value).GetComponent<TextMeshProUGUI>();
        cImg = GameObject.Find("CardImg" + value).GetComponent<Image>();
        value++;
    }
    private void FixedUpdate()
    {
        cCost.text = thisCard.cost.ToString();
        cTitle.text = thisCard.title.ToString();
        cType.text = thisCard.type.ToString();
        cText.text = thisCard.text.ToString();
        cImg.sprite = thisCard.cardImg;
    }

    public void UseThisCard()
    {

    }
}
