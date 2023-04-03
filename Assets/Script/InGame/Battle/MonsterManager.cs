using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IntentType { ATK = 0, DEF, BUF, DBUF }
[System.Serializable]
public class MonsterInfo
{
    string name;
    int hp;
    int atk;
    int def;
    Image img;
    IntentType type;
    Image intent_Img;
    List<int> skill;

    public void InputInfo(string _name,int _hp,int _atk, int _def, Image _img,IntentType _type,Image _intent_Img, List<int> _skill)
    {
        name = _name;
        hp = _hp;
        atk = _atk;
        def = _def;
        img = _img;
        type = _type;
        intent_Img = _intent_Img;
        skill = _skill;
    }
}
public class MonsterManager : MonoBehaviour
{
    //모든몬스터의 정보저장
    
}
