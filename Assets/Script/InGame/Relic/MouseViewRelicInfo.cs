using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseViewRelicInfo : MonoBehaviour
{
    private void Start()
    {
        InGame.Instance.relicInfoUi.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // ī�޶󿡼� ���̸� �߻��Ͽ� ���콺 ������ ��ġ���� ȭ������ ���ϵ��� �մϴ�.
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // �浹 ������ ������ ������ �����մϴ�.
        RaycastHit hit;

        // ����ĳ��Ʈ�� ����Ͽ� ���콺 ������ ��ġ���� ȭ������ ���ϴ� ���̸� �߻��ϰ�, �浹 �˻縦 �����մϴ�.
        if (Physics.Raycast(ray, out hit))
        {
            // �浹�� ��ü�� ����մϴ�.
            Debug.Log("�浹�� ��ü: " + hit.collider.gameObject.name);

            // ���⿡�� Ư�� ������Ʈ���� �浹�� �˻��ϰų� ���ϴ� �۾��� ������ �� �ֽ��ϴ�.
            if (hit.collider.gameObject.CompareTag("RelicBtn"))
            {
                InGame.Instance.relicInfoUi.GetComponent<RelicInfo>().relic = gameObject.GetComponent<RelicInfo>().relic;
                InGame.Instance.relicInfoUi.SetActive(true);
            }
        }
    }
}
