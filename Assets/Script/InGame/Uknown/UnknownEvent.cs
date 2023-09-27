using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownEvent : MonoBehaviour
{
    [SerializeField]
    int wog_GetGold = 75;
    [SerializeField]
    int wog_lostHp_Per = 15;

    [SerializeField]
    int cleric_HealPay = 35;
    [SerializeField]
    int cleric_GetHp_Per = 25;
    [SerializeField]
    int cleric_Purification_Pay = 50;

    [SerializeField]
    int wing_Statue_lostHp = 7;
    [SerializeField]
    int library_HealPer = 33;

    [SerializeField]
    GameObject buttonList_Goop;
    [SerializeField]
    GameObject buttonList_Cleric;
    [SerializeField]
    GameObject buttonList_Wing;
    [SerializeField]
    GameObject buttonList_Joust;
    [SerializeField]
    GameObject buttonList_Library;
    // Start is called before the first frame update

    [SerializeField]
    GameObject exitBtn;

    [SerializeField]
    UnknownManager unknownManager;

    [SerializeField]
    GameObject getCardView;

    private void Start()
    {
        exitBtn.SetActive(false);
    }
    public void ClearBtn()
    {
        buttonList_Goop.SetActive(true);
        buttonList_Cleric.SetActive(true);
        buttonList_Wing.SetActive(true);
        buttonList_Joust.SetActive(true);
        buttonList_Library.SetActive(true);
    }
    public void ChangeHp_fixedValue(int _hp)
    {
        InGame.Instance.charInfo.hp += _hp;
        breakthroughHp();
    }
    public void ChangeHp_PercentageValue(int _hp)
    {
        InGame.Instance.charInfo.hp += (int)(InGame.Instance.charInfo.maxHp * (_hp * 0.01));
        Debug.Log($"감소할hp:{_hp},현제hp:{InGame.Instance.charInfo.hp}");
        breakthroughHp();
    }
    void breakthroughHp()
    {
        if (InGame.Instance.charInfo.hp > InGame.Instance.charInfo.maxHp)
        {
            InGame.Instance.charInfo.hp = InGame.Instance.charInfo.maxHp;
        }
    }
    public void ChangeGold_fixedValue(int _gold)
    {
        InGame.Instance.charInfo.money += _gold;
        if (InGame.Instance.charInfo.money < 0)
        {
            InGame.Instance.charInfo.money = 0;
        }
    }
    public void DeleteCard()
    {
        unknownManager.unknownDisCard.DisCardViewBtn();
        unknownManager.unknownDisCard.CreateDisCardDeckObj();
    }

    public void World_Of_Goop_GetGold()//Btn
    {
        ChangeGold_fixedValue(wog_GetGold);
        ChangeHp_PercentageValue(wog_lostHp_Per * -1);
        buttonList_Goop.SetActive(false);
        exitBtn.SetActive(true);
    }
    public void World_Of_Goop_LostGold()//Btn
    {
        ChangeGold_fixedValue(CreateSeed.Instance.RandNum(20, 50));
        buttonList_Goop.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void The_Cleric_Heal()//Btn
    {
        ChangeGold_fixedValue(cleric_HealPay * -1);
        ChangeHp_PercentageValue(cleric_GetHp_Per);
        The_Cleric_Leave();
    }
    public void The_Cleric_Purification()//Btn
    {
        ChangeGold_fixedValue(cleric_Purification_Pay * -1);
        DeleteCard();
        The_Cleric_Leave();
    }

    public void The_Cleric_Leave()//Btn
    {
        buttonList_Cleric.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void Wing_Statue_Pray()//Btn
    {
        ChangeHp_fixedValue(wing_Statue_lostHp*-1);
        DeleteCard();
        Wing_Statue_Leave();
    }

    public void Wing_Statue_Broken()//Btn
    {
        ChangeGold_fixedValue(CreateSeed.Instance.RandNum(50, 80));
        Wing_Statue_Leave();
    }

    public void Wing_Statue_Leave()//Btn
    {
        buttonList_Wing.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void Joust_Betting(int index)//0=killer,1=master//Btn
    {
        int persent = 0;
        bool win;
        int killerGold = 100;
        int masterGold = 250;
        if (index == 0)
        {
            persent = 70;
            
        }
        else
        {
            persent = 30;
        }
        win = CreateSeed.Instance.Roulelet_Per(persent);
        if (win)
        {
            if(index == 0)
            {
                ChangeGold_fixedValue(killerGold);
            }
            else
            {
                ChangeGold_fixedValue(masterGold);
            }
            
        }
        else
        {
            Debug.Log("돈을 잃었습니다.....");
        }
        buttonList_Joust.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void SelectCard()
    {
        getCardView.SetActive(true);
    }

    public void Library_Read()//Btn
    {
        SelectCard();
        buttonList_Library.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void Library_Sleep()//Btn
    {
        ChangeHp_PercentageValue(library_HealPer);
        buttonList_Library.SetActive(false);
        exitBtn.SetActive(true);
    }
}
