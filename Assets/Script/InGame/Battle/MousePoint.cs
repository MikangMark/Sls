using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePoint : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 clickOffset;
    private Vector3 savePos;
    private OneCard card;
    private CardValueExcelDataLoader cardData;

    private void Start()
    {
        // UI 요소의 RectTransform 컴포넌트를 가져옵니다.
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        card = GetComponent<OneCard>();
        
        cardData = GameObject.Find("ExcelData").GetComponent<CardValueExcelDataLoader>();
    }
    private void Update()
    {
        if (InGame.Instance.openDeckView)
        {
            GetComponent<MousePoint>().enabled = false;
        }
        else
        {
            GetComponent<MousePoint>().enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 마우스 클릭 이벤트가 발생한 위치를 UI 요소의 부모 객체의 좌표계로 변환합니다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);
        savePos = rectTransform.localPosition;
        // UI 요소의 위치와 마우스 클릭 이벤트가 발생한 위치 사이의 차이를 구합니다.
        clickOffset = (Vector2)rectTransform.localPosition - localMousePosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 마우스 드래그 이벤트가 발생한 위치를 UI 요소의 부모 객체의 좌표계로 변환합니다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);

        // UI 요소의 위치를 마우스 드래그 이벤트가 발생한 위치로 이동합니다.
        rectTransform.localPosition = localMousePosition + clickOffset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        canvasGroup.blocksRaycasts = true;

        foreach (RaycastResult hit in results)
        {
            if(hit.gameObject.tag.Equals("Monster")|| hit.gameObject.tag.Equals("Player"))//레이캐스트의검출된 오브젝트의 태그 검사
            {
                if (gameObject.GetComponent<OneCard>().UseThisCard(hit.gameObject))//카드가 제데로 적동됬는지 여부검사
                {

                }
                else//실패했으경우 실패했다는 UI 표기
                {

                }
                
            }
        }
        rectTransform.localPosition = savePos;
    }
}