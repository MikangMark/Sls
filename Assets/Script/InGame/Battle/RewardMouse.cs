using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardMouse : MonoBehaviour
{
    [SerializeField]
    InGameUI inGameUI;
    public void SelectRewardCard()
    {
        Deck.Instance.AddDeck(gameObject.GetComponent<OneCard>().thisCard);
        gameObject.transform.parent.gameObject.SetActive(false);
        inGameUI.is_Reward = true;
    }
}