using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : Singleton<InGame>
{
    public Character.CharInfo charInfo;
    public bool openDeckView;
    public int currentFloor = 0;
    private void Awake()
    {
        Init();
        openDeckView = false;
        charInfo = Character.Instance.ironclead;
        //charInfo = Character.Instance.ironclead;
        switch (PlayerPrefs.GetInt("CharType")) //픽창에서선텍한 캐릭
        {
            case 0:
                charInfo = Character.Instance.ironclead;//아이언클래드
                break;
            case 1:
                charInfo = Character.Instance.silence;//사일런스
                break;
            case 2:
                charInfo = Character.Instance.defact;//디펙트
                break;
            case 3:
                charInfo = Character.Instance.wacher;//와쳐
                break;
            default:
                charInfo = Character.Instance.ironclead;//아이언클래드
                Debug.Log("DEFAULT");
                break;
        }
    }
}
