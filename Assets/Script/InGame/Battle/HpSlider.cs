using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpSlider : MonoBehaviour
{
    public Slider hpBar;
    public Monster monsterStat = null;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = gameObject.GetComponent<Slider>();
        
        switch (gameObject.tag)
        {
            case "Monster":
                monsterStat = gameObject.transform.parent.GetComponent<Monster>();
                hpBar.maxValue = monsterStat.stat.maxHp;
                text = hpBar.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                text.text = monsterStat.stat.hp + "/" + monsterStat.stat.maxHp;
                break;
            case "Player":
                hpBar.maxValue = InGame.Instance.charInfo.maxHp;
                text = hpBar.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                text.text = InGame.Instance.charInfo.hp + "/" + InGame.Instance.charInfo.maxHp;
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (gameObject.tag)
        {
            case "Monster":
                hpBar.value = monsterStat.stat.hp;
                break;
            case "Player":
                hpBar.value = InGame.Instance.charInfo.maxHp;
                break;
        }

        
    }
}
