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
    // Start is called before the first frame update

    [SerializeField]
    GameObject exitBtn;
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
    public void GetCard()
    {

    }
    public void DeleteCard()
    {

    }

    public void World_Of_Goop_GetGold()
    {
        ChangeGold_fixedValue(wog_GetGold);
        ChangeHp_PercentageValue(wog_lostHp_Per);
    }
    public void World_Of_Goop_LostGold()
    {
        ChangeGold_fixedValue(CreateSeed.Instance.RandNum(20, 50));
    }

    public void The_Cleric_Heal()
    {
        ChangeGold_fixedValue(cleric_HealPay * -1);
        ChangeHp_PercentageValue(cleric_GetHp_Per);
    }
    public void The_Cleric_Purification()
    {
        ChangeGold_fixedValue(cleric_Purification_Pay * -1);
        DeleteCard();
    }

    public void The_Cleric_Leave()
    {
        exitBtn.SetActive(true);
    }
}
