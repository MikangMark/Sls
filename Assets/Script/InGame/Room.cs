using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    /*
     방이 가지고있을 정보들
        1.이 방의 층위치
        2.방의 정보
        3.이방 다음과 연결될 방의 정보(들)->다음방과 연결될 방정보들을 받아서 선연결
        4.
     */
    public enum ROOMVALUE
    {
        //0.빈방, 1.일반적, 2.엘리트, 3.휴식, 4.상인, 5.미지, 6.보물, 7.보스
        EMPTY = 0, NOMAL, ELITE, REST, SHOP, UNKNOWN, TREASURE, BOSS
    }

    [SerializeField]
    int floor;
    [SerializeField]
    int value;
    List<Room> nextRoom;
    Room()
    {
        floor = -1;
        value = -1;
        nextRoom = new List<Room>();
    }
    public void SettingRoom(int _floor,int _value)
    {
        floor = _floor;
        value = _value;
        nextRoom = new List<Room>();
    }
    public void SettingNextRoom(List<Room> next)
    {
        for(int i = 0; i < next.Count; i++)
        {
            nextRoom.Add(next[i]);
        }
    }
}
