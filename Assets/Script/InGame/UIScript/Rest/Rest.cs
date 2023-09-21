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

    public void OnClickRestBtn()
    {
        float heal = 0.15f;
        int maxHp = InGame.Instance.charInfo.maxHp;
        if (InGame.Instance.charInfo.hp >= maxHp)
        {
            Debug.Log("�̹� �ִ�ü���Դϴ�");
        }
        else
        {
            InGame.Instance.charInfo.hp += (int)(maxHp * heal);
            if (InGame.Instance.charInfo.hp > maxHp)
            {
                InGame.Instance.charInfo.hp = maxHp;
            }
            Debug.Log("ü����ȸ���߽��ϴ�");
            restBtn.interactable = false;
        }
    }

    public void OnClickNextRestBtn()
    {
        InGame.Instance.currentFloor++;
        restBtn.interactable = true;
        restObj.SetActive(false);
    }
}
