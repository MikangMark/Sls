using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : Singleton<InGame>
{
    public Character.CharInfo charInfo;

    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("CharType")) 
        {
            case 0:
                charInfo = Character.Instance.ironclead;
                break;
            case 1:
                charInfo = Character.Instance.silence;
                break;
            case 2:
                charInfo = Character.Instance.defact;
                break;
            case 3:
                charInfo = Character.Instance.wacher;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
