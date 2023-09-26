using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownEvent : MonoBehaviour
{
    [SerializeField]
    int wog_GetGold = 75;
    [SerializeField]
    int wog_lostHp_Per = -15;

    [SerializeField]
    int cleric_HealPay = 35;
    [SerializeField]
    int cleric_GetHp_Per = 25;
    [SerializeField]
    int cleric_Purification_Pay = -50;

    [SerializeField]
    int wing_Statue_lostHp = -7;

    [SerializeField]
    int joust_Betting = 50;
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

    public void ChangeHp_fixedValue(int _hp)
    {
        InGame.Instance.charInfo.hp += _hp;
        breakthroughHp();
    }
    public void ChangeHp_PercentageValue(int _hp)
    {
        InGame.Instance.charInfo.hp += InGame.Instance.charInfo.maxHp * _hp;
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

    public void World_Of_Goop_GetGold()
    {
        ChangeGold_fixedValue(wog_GetGold);
        ChangeHp_PercentageValue(wog_lostHp_Per);
        buttonList_Goop.SetActive(false);
        exitBtn.SetActive(true);
    }
    public void World_Of_Goop_LostGold()
    {
        ChangeGold_fixedValue(CreateSeed.Instance.RandNum(20, 50));
        buttonList_Goop.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void The_Cleric_Heal()
    {
        ChangeGold_fixedValue(cleric_HealPay * -1);
        ChangeHp_PercentageValue(cleric_GetHp_Per);
        The_Cleric_Leave();
    }
    public void The_Cleric_Purification()
    {
        ChangeGold_fixedValue(cleric_Purification_Pay);
        DeleteCard();
        The_Cleric_Leave();
    }

    public void The_Cleric_Leave()
    {
        buttonList_Cleric.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void Wing_Statue_Pray()
    {
        ChangeHp_fixedValue(wing_Statue_lostHp);
        DeleteCard();
        Wing_Statue_Leave();
    }

    public void Wing_Statue_Broken()
    {
        ChangeGold_fixedValue(CreateSeed.Instance.RandNum(50, 80));
        Wing_Statue_Leave();
    }

    public void Wing_Statue_Leave()
    {
        buttonList_Wing.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void Joust_Betting(int index)//0=killer,1=master
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
            Debug.Log("���� �Ҿ����ϴ�.....");
        }
        buttonList_Joust.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void SelectCard()
    {
        getCardView.SetActive(true);
    }

    public void Library_Read()
    {
        SelectCard();
        buttonList_Library.SetActive(false);
        exitBtn.SetActive(true);
    }

    public void Library_Sleep()
    {
        ChangeHp_PercentageValue(library_HealPer);
        buttonList_Library.SetActive(false);
        exitBtn.SetActive(true);
    }
}
