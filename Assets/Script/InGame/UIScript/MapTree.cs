using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class MapNode//���ϳ��� ����
{
    public enum ROOMVALUE
    {
        //0.���, 1.�Ϲ���, 2.����Ʈ, 3.�޽�, 4.����, 5.����, 6.����, 7.����
        DEFAULT = -1, EMPTY = 0, NOMAL, ELITE, REST, SHOP, UNKNOWN, TREASURE, BOSS
    }
    public int roomNum;
    public string roomName; // �� �̸�
    [HideInInspector]
    public List<MapNode> children; // �ڽ� �� ����Ʈ
    [HideInInspector]
    public List<MapNode> parent;//�θ�� ����Ʈ
    public List<GameObject> childrenObj;
    public int floor;//���� ��
    public ROOMVALUE roomType;
    public Sprite room_Img;
    public GameObject roomObj;

    public MapNode(MapNode map)
    {
        roomNum = map.roomNum;
        roomName = map.roomName;
        children = map.children;
        floor = map.floor;
        roomType = map.roomType;
        room_Img = map.room_Img;
        roomObj = map.roomObj;
        childrenObj = map.childrenObj;
    }
    public MapNode()
    {
        roomNum = 0;
        roomName = "";
        children = new List<MapNode>();
        childrenObj = new List<GameObject>();
        floor = 0;
        roomType = ROOMVALUE.DEFAULT;
        room_Img = null;
        roomObj = null;
    }
    // �ڽ� ��ų �߰�
    public void AddChild(MapNode child)
    {
        children.Add(child);
    }

    // �ڽ� ��ų ����
    public void RemoveChild(MapNode child)
    {
        children.Remove(child);
    }

    public void InputData(MapNode _data)
    {
        roomNum = _data.roomNum;
        roomName = _data.roomName;
        children = _data.children;
        floor = _data.floor;
        roomType = _data.roomType;
        room_Img = _data.room_Img;
        roomObj = _data.roomObj;
        childrenObj = _data.childrenObj;
    }
}
public class MapTree//��Ʈ��ų����
{
    public MapNode root; // ��Ʈ ��ų

    // ��ų �߰�
    public void AddSkill(MapNode parentRoom, MapNode newRoom)
    {
        MapNode parentNode = FindSkillNode(root, parentRoom.roomNum);

        if (parentNode != null)
        {
            MapNode tempRoom = new MapNode(newRoom);
            parentNode.AddChild(tempRoom);
        }
    }

    // ��ų ����
    public void RemoveSkill(int roomNum)
    {
        MapNode parentNode = FindParentNode(root, roomNum);

        if (parentNode != null)
        {
            MapNode mapNode = FindSkillNode(parentNode, roomNum);
            parentNode.RemoveChild(mapNode);
        }
    }

    // ��ų ��� �˻�
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

    // �θ� ��� �˻�
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