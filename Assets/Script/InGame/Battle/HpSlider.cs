using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    public Slider hpBar;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = gameObject.GetComponent<Slider>();
        switch (gameObject.tag)
        {
            case "Monster":
                
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
