using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class UnknownDisCardClick : MonoBehaviour, IPointerClickHandler
{
    UnknownDisCard unknown;
    OneCard thisCard;
    [SerializeField]
    GameObject disCardWarringView;
    void Start()
    {
        thisCard = gameObject.GetComponent<OneCard>();
        unknown = GameObject.Find("UknownScript").GetComponent<UnknownManager>().unknownDisCard;
        disCardWarringView = InGame.Instance.UnknownDisCardWarringWiew;
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
                unknown.SetDisCardTarget(thisCard.thisCard);
            }
        }
    }

    public void PlayDisCard()
    {
        Deck.Instance.DisCardDeck(thisCard.thisCard);
    }
}
