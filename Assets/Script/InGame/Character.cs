using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    enum CharacterType { IRONCLEAD = 0, SILENCE, DEFACT, WACHER }

    int hp;
    int maxHp;
    int money;
    int card;
    CharacterType charType;

}
