using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    public Slider hpBar;
    public Monster monsterStat;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = gameObject.GetComponent<Slider>();
        switch (gameObject.tag)
        {
            case "Monster":
                monsterStat = gameObject.GetComponent<Monster>();
                hpBar.maxValue = monsterStat.stat.maxHp;
                //hpBar.value = 
                break;
            case "Player":
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
