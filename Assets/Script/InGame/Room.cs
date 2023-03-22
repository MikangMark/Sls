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
    public enum ROOMVALUE
    {
        //0.���, 1.�Ϲ���, 2.����Ʈ, 3.�޽�, 4.����, 5.����, 6.����, 7.����
        DEFAULT = -1, EMPTY = 0, NOMAL, ELITE, REST, SHOP, UNKNOWN, TREASURE, BOSS
    }

    [SerializeField]
    int floor;
    [SerializeField]
    ROOMVALUE value;
    List<Room> nextRoom;
    Room()
    {
        floor = -1;
        value = ROOMVALUE.DEFAULT;
        nextRoom = new List<Room>();
    }
    
    public void SettingRoom(int _floor, ROOMVALUE _value)
    {
        floor = _floor;
        value = _value;
    }
    public void SettingNextRoom(List<Room> next)
    {
        for(int i = 0; i < next.Count; i++)
        {
            nextRoom.Add(next[i]);
        }
    }
}
