using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    /*
     ���� ���������� ������
        1.�� ���� ����ġ
        2.���� ����
        3.�̹� ������ ����� ���� ����(��)->������� ����� ���������� �޾Ƽ� ������
        4.
     */

    [SerializeField]
    MapNode node;
   
    public void SettingRoom(MapNode data)
    {
        node = data;
    }
}
