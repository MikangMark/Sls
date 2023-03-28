using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneCard : MonoBehaviour
{
    public CardInfo thisCard;
    public TextMeshProUGUI cCost;
    public TextMeshProUGUI cTitle;
    public TextMeshProUGUI cType;
    public TextMeshProUGUI cText;
    public Image cImg;
    /*
    public int cost;
    public string title;
    public Type type;
    public string text;
    public Sprite cardImg;
    */
    private void Start()
    {
        cCost = GameObject.Find("CostText").GetComponent<TextMeshProUGUI>();
        cTitle = GameObject.Find("CardTitle").GetComponent<TextMeshProUGUI>();
        cType = GameObject.Find("CardType").GetComponent<TextMeshProUGUI>();
        cText = GameObject.Find("CardText").GetComponent<TextMeshProUGUI>();
        cImg = GameObject.Find("CardImg").GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        cCost.text = thisCard.cost.ToString();
        cTitle.text = thisCard.title.ToString();
        cType.text = thisCard.type.ToString();
        cText.text = thisCard.text.ToString();
        cImg.sprite = thisCard.cardImg;
    }
}
