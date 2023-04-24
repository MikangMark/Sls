using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class MapNode//방하나당 정보
{
    public enum ROOMVALUE
    {
        //0.빈방, 1.일반적, 2.엘리트, 3.휴식, 4.상인, 5.미지, 6.보물, 7.보스
        DEFAULT = -1, EMPTY = 0, NOMAL, ELITE, REST, SHOP, UNKNOWN, TREASURE, BOSS
    }
    public int roomNum;
    public string roomName; // 방 이름
    public List<MapNode> children; // 자식 방 리스트
    public int depth;//방의 층
    public ROOMVALUE roomType;
    public Image room_Img;
    public GameObject roomObj;

    public MapNode(MapNode map)
    {
        roomNum = map.roomNum;
        roomName = map.roomName;
        children = map.children;
        depth = map.depth;
        roomType = map.roomType;
        room_Img = map.room_Img;
        roomObj = map.roomObj;
    }
    // 자식 스킬 추가
    public void AddChild(MapNode child)
    {
        children.Add(child);
    }

    // 자식 스킬 삭제
    public void RemoveChild(MapNode child)
    {
        children.Remove(child);
    }

    public void InputData(MapNode _data)
    {
        roomNum = _data.roomNum;
        roomName = _data.roomName;
        children = _data.children;
        depth = _data.depth;
        roomType = _data.roomType;
        room_Img = _data.room_Img;
        roomObj = _data.roomObj;
    }
}
public class MapTree//루트스킬관리
{
    public MapNode root; // 루트 스킬

    // 스킬 추가
    public void AddSkill(MapNode parentRoom, MapNode newRoom)
    {
        MapNode parentNode = FindSkillNode(root, parentRoom.roomNum);

        if (parentNode != null)
        {
            MapNode tempRoom = new MapNode(newRoom);
            parentNode.AddChild(tempRoom);
        }
    }

    // 스킬 삭제
    public void RemoveSkill(int roomNum)
    {
        MapNode parentNode = FindParentNode(root, roomNum);

        if (parentNode != null)
        {
            MapNode mapNode = FindSkillNode(parentNode, roomNum);
            parentNode.RemoveChild(mapNode);
        }
    }

    // 스킬 노드 검색
    private MapNode FindSkillNode(MapNode node, int roomNum)
    {
        if (node.roomNum == roomNum)
        {
            return node;
        }

        foreach (MapNode child in node.children)
        {
            MapNode result = FindSkillNode(child, roomNum);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    // 부모 노드 검색
    private MapNode FindParentNode(MapNode node, int roomNum)
    {
        foreach (MapNode child in node.children)
        {
            if (child.roomNum == roomNum)
            {
                return node;
            }

            MapNode result = FindParentNode(child, roomNum);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
