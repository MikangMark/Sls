using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    private Battle battle;
    private GameObject shieldObj;
    [SerializeField]
    private GameObject monsterIntentPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (battle.shiled > 0)
        {
            shieldObj.SetActive(true);
        }
    }
}
