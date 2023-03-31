using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public enum IntentType { ATK = 0, DEF, BUF, DBUF }
    public struct monsterInfo
    {
        string name;
        int hp;
        int atk;
        int def;
        Image img;
        IntentType type;
        Image intent_Img;
        int skill;
    }

    //�����Ѹ������� �� ��ũ��Ʈ�� ����
    //���� ���ݼ�ġ ü�� ����ġ ����ϴ� �����̹��� �ǵ��̹���(Intent) �ǵ�Ÿ�� ����ϴ½�ų�ڵ�
}
