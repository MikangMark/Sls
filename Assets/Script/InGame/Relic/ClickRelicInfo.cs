using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRelicInfo : MonoBehaviour
{
    public void OnClickRelicInfoVeiw(RelicInfo relicInfo)
    {
        InGame.Instance.relicInfoUi.GetComponent<RelicInfo>().relic = relicInfo.relic;
        InGame.Instance.relicInfoUi.SetActive(true);
    }
    public void ClosedRelicInfo()
    {
        InGame.Instance.relicInfoUi.SetActive(false);
    }
}
