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
        // UI ����� RectTransform ������Ʈ�� �����ɴϴ�.
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
        // ���콺 Ŭ�� �̺�Ʈ�� �߻��� ��ġ�� UI ����� �θ� ��ü�� ��ǥ��� ��ȯ�մϴ�.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);
        savePos = rectTransform.localPosition;
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
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        canvasGroup.blocksRaycasts = true;

        foreach (RaycastResult hit in results)
        {
            if(hit.gameObject.tag.Equals("Monster")|| hit.gameObject.tag.Equals("Player"))//����ĳ��Ʈ�ǰ���� ������Ʈ�� �±� �˻�
            {
                if (gameObject.GetComponent<OneCard>().UseThisCard(hit.gameObject))//ī�尡 ������ ��������� ���ΰ˻�
                {

                }
                else//����������� �����ߴٴ� UI ǥ��
                {

                }
                
            }
        }
        rectTransform.localPosition = savePos;
    }
}