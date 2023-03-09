using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : Singleton<InGame>
{
    Character.CharInfo charInfo;
    Character character;
    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("CharType")) 
        {
            case 0:
                charInfo = character.ironclead;
                break;
            case 1:
                charInfo = character.silence;
                break;
            case 2:
                charInfo = character.defact;
                break;
            case 3:
                charInfo = character.wacher;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
