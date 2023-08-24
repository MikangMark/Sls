using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopClickCard : MonoBehaviour,IPointerClickHandler
{
    OneCard thisCard;
    void Start()
    {
        thisCard = gameObject.GetComponent<OneCard>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        foreach (RaycastResult hit in results)
        {
            if (hit.gameObject == gameObject)//레이캐스트의검출된 오브젝트의 태그 검사
            {
                if (gameObject.GetComponent<OneCard>().thisCard.shop <= InGame.Instance.charInfo.money)
                {
                    InGame.Instance.charInfo.money -= gameObject.GetComponent<OneCard>().thisCard.shop;
                    Deck.Instance.AddDeck(gameObject.GetComponent<OneCard>().thisCard);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
