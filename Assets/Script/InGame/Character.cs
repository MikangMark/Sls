using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    public enum CharType { IRONCLEAD = 0, SILENCE, DEFACT, WACHER }
    [System.Serializable]
    public struct CharInfo
    {
        public int hp;
        public int maxHp;
        public int money;
        public CharType charType;
    }

    public CharInfo ironclead;
    public CharInfo silence;
    public CharInfo defact;
    public CharInfo wacher;
    private void Awake()
    {
        Init();
        ironclead.charType = CharType.IRONCLEAD;
        ironclead.hp = 80;
        ironclead.maxHp = 80;
        ironclead.money = 99;

        silence.charType = CharType.SILENCE;
        silence.hp = 70;
        silence.maxHp = 70;
        silence.money = 99;

        defact.charType = CharType.DEFACT;
        defact.hp = 75;
        defact.maxHp = 75;
        defact.money = 99;

        wacher.charType = CharType.WACHER;
        wacher.hp = 72;
        wacher.maxHp = 72;
        wacher.money = 99;
    }
}
