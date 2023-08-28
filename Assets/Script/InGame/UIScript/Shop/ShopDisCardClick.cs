using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopDisCardClick : MonoBehaviour, IPointerClickHandler
{
    ShopScript shop;
    OneCard thisCard;
    [SerializeField]
    GameObject disCardWarringView;
    int onece = 0;
    void Start()
    {
        thisCard = gameObject.GetComponent<OneCard>();
        shop = InGame.Instance.shopscript;
        disCardWarringView = InGame.Instance.disCardWarringWiew;
        disCardWarringView.SetActive(false);
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
                disCardWarringView.SetActive(true);
                disCardWarringView.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = thisCard.thisCard.title + " 이 카드를 제거 하시겠습니까?";
                shop.SetDisCardTarget(thisCard.thisCard);
            }
        }
    }

    public void PlayDisCard()
    {
        Deck.Instance.DisCardDeck(thisCard.thisCard);
    }
}
