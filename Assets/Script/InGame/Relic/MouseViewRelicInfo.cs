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

        // 카메라에서 레이를 발사하여 마우스 포인터 위치에서 화면으로 향하도록 합니다.
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // 충돌 정보를 저장할 변수를 선언합니다.
        RaycastHit hit;

        // 레이캐스트를 사용하여 마우스 포인터 위치에서 화면으로 향하는 레이를 발사하고, 충돌 검사를 수행합니다.
        if (Physics.Raycast(ray, out hit))
        {
            // 충돌한 객체를 출력합니다.
            Debug.Log("충돌한 객체: " + hit.collider.gameObject.name);

            // 여기에서 특정 오브젝트와의 충돌을 검사하거나 원하는 작업을 수행할 수 있습니다.
            if (hit.collider.gameObject.CompareTag("RelicBtn"))
            {
                InGame.Instance.relicInfoUi.GetComponent<RelicInfo>().relic = gameObject.GetComponent<RelicInfo>().relic;
                InGame.Instance.relicInfoUi.SetActive(true);
            }
        }
    }
}
