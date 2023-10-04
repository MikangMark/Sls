using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnAbleTreasure : MonoBehaviour
{
    [SerializeField]
    TreasureUi treasureUi;
    [SerializeField]
    GameObject inGameRelicParent;
    [SerializeField]
    GameObject relicPrf;
    [SerializeField]
    GameObject blackBackGround;
    [SerializeField]
    GameObject treasureRewardBackGround;

    public List<int> setRandIndexList;
    void OnEnable()
    {
        InGame.Instance.ClearRelicReward();
        InGame.Instance.SetTreasureRelic();
        treasureUi.SetRewardBtn(InGame.Instance.treasureRewardList);
        treasureUi.treasureListParent.SetActive(true);
        setRandIndexList.Clear();
        treasureUi.nextBtn.gameObject.SetActive(false);
        treasureRewardBackGround.SetActive(false);
        blackBackGround.SetActive(false);
    }

    private void OnDisable()
    {
        treasureUi.treasureListParent.SetActive(false);
        treasureUi.treasureBtn.gameObject.GetComponent<Image>().sprite = treasureUi.box;
    }

    public void GetRelicObj(int _relicIndex)
    {
        GameObject temp = Instantiate(relicPrf, inGameRelicParent.transform);
        temp.GetComponent<RelicInfo>().relic = InGame.Instance.relicExcelDataLoader.relicInfo[InGame.Instance.relicIndexList[_relicIndex]];
        InGame.Instance.haveRelicList.Add(temp);
        for(int i = 0;i< treasureUi.getTreasureBtn.Count; i++)
        {
            treasureUi.getTreasureBtn[i].SetActive(false);
        }
        blackBackGround.SetActive(false);
        treasureRewardBackGround.SetActive(false);
        treasureUi.nextBtn.gameObject.SetActive(true);
    }

    public void OnClickTreasureBox()
    {
        blackBackGround.SetActive(true);
        treasureRewardBackGround.SetActive(true);
        treasureUi.treasureBtn.gameObject.GetComponent<Image>().sprite = treasureUi.boxOpen;
    }
}
