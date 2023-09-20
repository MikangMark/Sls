using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rest : MonoBehaviour
{
    [SerializeField]
    GameObject restObj;
    [SerializeField]
    Button restBtn;

    public void OnCLickRestBtn()
    {
        float heal = 0.15f;
        int realHp = InGame.Instance.charInfo.hp;
        int maxHp = InGame.Instance.charInfo.maxHp;
        if (realHp >= maxHp)
        {
            Debug.Log("이미 최대체력입니다");
        }
        else
        {
            realHp += (int)(maxHp * heal);
            if (realHp > maxHp)
            {
                realHp = maxHp;
            }
            Debug.Log("체력을회복했습니다");
            restBtn.interactable = false;
        }
    }

    public void OnClickNextRestBtn()
    {
        restBtn.interactable = true;
        restObj.SetActive(false);
    }
}
