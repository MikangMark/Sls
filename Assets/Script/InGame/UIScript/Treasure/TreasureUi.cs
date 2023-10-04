using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureUi : MonoBehaviour
{
    public Button treasureBtn;
    public Button nextBtn;
    public Sprite box;
    public Sprite boxOpen;
    public GameObject treasureListParent;
    public List<GameObject> getTreasureBtn;
    public GameObject treasureBackGround;

    public void SetRewardBtn(List<Relic> _relics)
    {
        if(_relics != null)
        {
            for (int i = 0; i < _relics.Count; i++)
            {
                getTreasureBtn[i].GetComponent<RelicInfo>().relic = _relics[i];
            }
        }
    }

    public void ExitTreasure()
    {
        treasureBackGround.SetActive(false);
        InGame.Instance.currentFloor++;
    }
}
