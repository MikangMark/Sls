using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePoint : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 clickOffset;

    private void Start()
    {
        // UI ����� RectTransform ������Ʈ�� �����ɴϴ�.
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ���콺 Ŭ�� �̺�Ʈ�� �߻��� ��ġ�� UI ����� �θ� ��ü�� ��ǥ��� ��ȯ�մϴ�.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);

        // UI ����� ��ġ�� ���콺 Ŭ�� �̺�Ʈ�� �߻��� ��ġ ������ ���̸� ���մϴ�.
        clickOffset = (Vector2)rectTransform.localPosition - localMousePosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���콺 �巡�� �̺�Ʈ�� �߻��� ��ġ�� UI ����� �θ� ��ü�� ��ǥ��� ��ȯ�մϴ�.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);

        // UI ����� ��ġ�� ���콺 �巡�� �̺�Ʈ�� �߻��� ��ġ�� �̵��մϴ�.
        rectTransform.localPosition = localMousePosition + clickOffset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���콺 Ŭ�� �̺�Ʈ�� �߻��� ��ġ�� UI ����� �θ� ��ü�� ��ǥ��� ��ȯ�մϴ�.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);

        // UI ����� ��ġ�� ���콺 Ŭ�� �̺�Ʈ�� �߻��� ��ġ�� �̵��մϴ�.
        rectTransform.localPosition = localMousePosition + clickOffset;
        canvasGroup.blocksRaycasts = true;
        RaycastHit2D[] hits = Physics2D.RaycastAll(localMousePosition, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Monster"))
            {
                Debug.Log("Dropped on target object: " + hit.transform.name);
                return;
            }
        }
        Debug.Log(hits.Length);
        Debug.Log("Did not drop on any target object");
    }
}