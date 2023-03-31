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

    private void Start()
    {
        // UI 요소의 RectTransform 컴포넌트를 가져옵니다.
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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
        // 마우스 클릭 이벤트가 발생한 위치를 UI 요소의 부모 객체의 좌표계로 변환합니다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);

        // UI 요소의 위치를 마우스 클릭 이벤트가 발생한 위치로 이동합니다.
        rectTransform.localPosition = localMousePosition + clickOffset;
        canvasGroup.blocksRaycasts = true;
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(localMousePosition, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Monster"))
            {
                Debug.Log("Dropped on target object: " + hit.transform.name);
                gameObject.transform.SetParent(gameObject.transform.parent);
                return;
            }
        }
        Debug.Log(hits.Length);
        Debug.Log("Did not drop on any target object");
        rectTransform.localPosition = savePos;
    }
}