using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBtn : MonoBehaviour
{
    public InGameUI inGameUI;
    private void Start()
    {
        inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
    }
    private void Update()
    {
        if (inGameUI.is_Reward)
        {
            inGameUI.selectedReward.SetActive(false);
        }
    }
    public void OnClickGetReward_Gold()
    {
        InGame.Instance.charInfo.money += InGame.Instance.rewardGold;
        GameObject.Find("RewardGold").SetActive(false);
    }
    public void OnClickGetReward_Cards()
    {
        inGameUI.OnClickCardReward();
        inGameUI.selectedReward = gameObject;
        char[] index = gameObject.name.ToCharArray();
        inGameUI.SetRewardCards(int.Parse(index[14].ToString()) - 1);
        
    }
}