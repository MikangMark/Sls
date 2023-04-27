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

    public MapNode node;
    Room()
    {
        node = new MapNode();
    }
   
    public void SettingRoom(MapNode data)
    {
        node = data;
    }
}
