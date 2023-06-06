using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ROOMVALUE = MapNode.ROOMVALUE;


public class MapCreate : MonoBehaviour
{

    //�������� 8���� ���� ���� ���� �޽� �Ϲ��� ����Ʈ ���� ���
    //0.���, 1.�Ϲ���, 2.����Ʈ, 3.�޽�, 4.����, 5.����, 6.����, 7.����
    //���������Ǵ°��� ������ ������ 6����
    //0���� �Ϲ������� ����, 13���� �޽����� ����, 14���� ���������ΰ��� 1���ΰ���

    /*
     * ó�� ����ް� �Ѿ�¹� �������ʴ� 0����
     * ������ �̸� ���������Ǿ��ִ¹�� ���� �����ϴ� ��
     1. ���� ��������� ����
        �� ���� �����ϸ� �켱 ���� 7 x ���� 15¥�� ���ڸ� ����. �̰� �츮�� ���� �Ǵ� ���� ����.
        �� ������ (���ϵ������� ������ �̷�?? �ΰ� �ƹ�ư �� ���ڷ� ����� �ɼ� �����ִ°� �ƴ϶��)
        ���ι������� y��ǥ 1���� 15���� ��θ� �� ����.
        �� ��δ� y������ 1��ŭ �����Ҷ� x��ǥ�� -1���� 1���� �����ϰ� ���ϸ鼭 �ö�.
        �̰� �� 6�� �ݺ��� ������ ��λ� �ִ� ��� ���������� ������ ������� ����.
     */
    static int floor = 16;//0�����̶� 15������ 1���ΰ����� ��������
    public GameObject room_p;
    public GameObject floor_p;
    public List<GameObject> floor_List;
    public GameObject floorObj_Parents;
    public List<GameObject> room_List;
    public MapTree mapTree;
    public int createRoomCount = 0;
    public List<int> roomTypeRand = new List<int>();
    public List<int> floor2roomCount;

    //0���� ���� �Ϲ�����������
    //8���� ���� ����
    //14���� �޽ĸ���������
    //15���� ���� �Ѱ�����������
    //8������ ������ �������� 0~5����
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i == 0 || i == 6 || i == 7)
            {
                continue;
            }
            roomTypeRand.Add(i);
        }
        mapTree = new MapTree();
        SetFloorObject();
        floor2roomCount = new List<int>();
        room_List = new List<GameObject>();
        for (int i = 0; i < floor; i++)
        {
            if (i == floor - 1)
            {
                floor2roomCount.Add(1);
            }
            else
            {
                int roomCount = CreateSeed.Instance.RandNum(2, 7);
                floor2roomCount.Add(roomCount);
            }
        }
        RootCreate();
        for(int i = floor - 1; i > 0; i--)
        {
            CreateLine(i, i - 1);
        }

    }
    #region RoomCreate
    void SetFloorObject()
    {

        for (int i = 0; i < floor; i++)
        {
            floor_List.Add(null);
        }
        for (int i = floor - 1; i >= 0; i--)//��������� ���� �ε����� ������
        {
            GameObject floorObj = Instantiate(floor_p, floorObj_Parents.transform);
            floorObj.name = "Floor[" + i + "]";
            floor_List[i] = floorObj;
        }
    }
    void RootCreate()
    {
        for (int i = floor - 1; i >= 0; i--)
        {
            for (int j = 0; j < floor2roomCount[i]; j++)
            {
                SetRoomObject(i, j);
            }
        }
    }
    void SetRoomObject(int i, int j)//���� ������Ʈ ����
    {
        //����������Ʈ ����
        //�ֻ��� ���� ����(���������)
        //�����γ������½����� �����
        //���ϳ����鶧���� �̽�ũ��Ʈ�� ����
        GameObject roomObj = Instantiate(room_p, floor_List[i].transform);

        switch (i)
        {
            case 15:
                roomObj.GetComponent<Room>().node.roomType = ROOMVALUE.BOSS;
                break;
            case 14:
                roomObj.GetComponent<Room>().node.roomType = ROOMVALUE.REST;
                break;
            case 8:
                roomObj.GetComponent<Room>().node.roomType = ROOMVALUE.TREASURE;
                break;
            case 0:
                roomObj.GetComponent<Room>().node.roomType = ROOMVALUE.NOMAL;
                break;
            default:
                roomObj.GetComponent<Room>().node.roomType = (ROOMVALUE)CreateSeed.Instance.RandNum(roomTypeRand);
                break;
        }
        roomObj.GetComponent<Room>().node.roomNum = createRoomCount;
        roomObj.GetComponent<Room>().node.roomName = "[" + i + "][" + j + "]" + roomObj.GetComponent<Room>().node.roomType.ToString();
        roomObj.GetComponent<Room>().node.floor = i;
        roomObj.GetComponent<Room>().node.children = new List<MapNode>();
        roomObj.name = roomObj.GetComponent<Room>().node.roomName;
        createRoomCount++;
        room_List.Add(roomObj);

    }
    #endregion

    /*
        ���� �̹� �����Ǿ��ִ�
        ���� �������� �����Ǵ� ���ǰ����� �ּ�3������ 6�������̴�
        ���� ������ ���� ������ �������� ���ǰ����� 3�̻��ϰ�� ���� �ּ� ������ �氳�������� ���� �氳���� �ȴ�
        ������ ���� �����Ǿ�� �ȵȴ�
        �ѹ濡�� ���������� ����Ǵ� ���ǰ����� 3������ �����ϴ�
     */
    void CreateLine(int curLayer, int nextLayer)
    {
        int minLine = 3;
        int largeLayer;
        int smallLayer;
        int createline = 0;
        int maxLine = 6;
        int q;
        if (floor2roomCount[curLayer] >= floor2roomCount[nextLayer])
        {
            largeLayer = curLayer;
            smallLayer = nextLayer;
            if (minLine < floor2roomCount[largeLayer])
            {
                minLine = floor2roomCount[largeLayer];
            }
        }
        else
        {
            largeLayer = nextLayer;
            smallLayer = curLayer;
            if (minLine < floor2roomCount[largeLayer])
            {
                minLine = floor2roomCount[largeLayer];
            }
        }
        if (floor2roomCount[curLayer] == 2)
        {
            if (floor2roomCount[nextLayer] == 2)
            {
                maxLine = 3;
            }
            else if (floor2roomCount[nextLayer] == 3)
            {
                maxLine = 4;
            }
        }
        else if (floor2roomCount[curLayer] == 3)
        {
            if (floor2roomCount[nextLayer] == 3)
            {
                maxLine = 5;
            }
        }
        if (floor2roomCount[nextLayer] == 2)
        {
            if (floor2roomCount[curLayer] == 2)
            {
                maxLine = 3;
            }
            else if (floor2roomCount[curLayer] == 3)
            {
                maxLine = 4;
            }
        }
        else if (floor2roomCount[nextLayer] == 3)
        {
            if (floor2roomCount[curLayer] == 3)
            {
                maxLine = 5;
            }
        }
        createline = CreateSeed.Instance.RandNum(minLine, maxLine);
        q = floor2roomCount[largeLayer] / floor2roomCount[smallLayer];
        if (floor2roomCount[smallLayer] == 1)
        {
            for (int i = 0; i < floor2roomCount[largeLayer]; i++)
            {
                floor_List[smallLayer].transform.GetChild(0).gameObject.GetComponent<LineScript>().target.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                floor_List[largeLayer].transform.GetChild(i).gameObject.GetComponent<Room>().paretRooms.Add(floor_List[smallLayer].transform.GetChild(0).gameObject);
                floor_List[smallLayer].transform.GetChild(0).gameObject.GetComponent<Room>().childRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                createline--;
            }

        }
        else
        {
            for (int i = 0; i < floor2roomCount[smallLayer]; i++)
            {
                floor_List[smallLayer].transform.GetChild(i).gameObject.GetComponent<LineScript>().target.Add(floor_List[largeLayer].transform.GetChild(i * q).gameObject);
                if (smallLayer < largeLayer)
                {
                    floor_List[largeLayer].transform.GetChild(i * q).gameObject.GetComponent<Room>().childRooms.Add(floor_List[smallLayer].transform.GetChild(i).gameObject);
                    floor_List[smallLayer].transform.GetChild(i).gameObject.GetComponent<Room>().paretRooms.Add(floor_List[largeLayer].transform.GetChild(i * q).gameObject);
                }
                else
                {
                    floor_List[smallLayer].transform.GetChild(i).gameObject.GetComponent<Room>().childRooms.Add(floor_List[largeLayer].transform.GetChild(i * q).gameObject);
                    floor_List[largeLayer].transform.GetChild(i * q).gameObject.GetComponent<Room>().paretRooms.Add(floor_List[smallLayer].transform.GetChild(i).gameObject);
                }
                createline--;
            }
        }
        for (int i = 0; i < floor_List[largeLayer].transform.childCount; i++)
        {

            if (floor2roomCount[curLayer] > floor2roomCount[nextLayer])//���ﰢ
            {
                if (floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().childRooms.Count < 1)
                {
                    switch (q)
                    {
                        case 1:
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().childRooms.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                            floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject.GetComponent<Room>().paretRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                            createline--;
                            break;
                        case 2:
                            if (floor_List[smallLayer].transform.childCount == 2)
                            {
                                if (i == 4)
                                {
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().childRooms.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                                    floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).GetComponent<Room>().paretRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                                    createline--;
                                }
                                else
                                {
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(i / q).gameObject);
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().childRooms.Add(floor_List[smallLayer].transform.GetChild(i / q).gameObject);
                                    floor_List[smallLayer].transform.GetChild(i / q).GetComponent<Room>().paretRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                                    createline--;
                                }

                            }
                            else if (floor_List[smallLayer].transform.childCount == 3)
                            {
                                floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(i / q).gameObject);
                                floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().childRooms.Add(floor_List[smallLayer].transform.GetChild(i / q).gameObject);
                                floor_List[smallLayer].transform.GetChild(i / q).GetComponent<Room>().paretRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                                createline--;
                            }
                            break;
                        case 3:
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(i / q).gameObject);
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().childRooms.Add(floor_List[smallLayer].transform.GetChild(i / q).gameObject);
                            floor_List[smallLayer].transform.GetChild(i / q).GetComponent<Room>().paretRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                            createline--;
                            break;
                    }

                }
            }
            else if (floor2roomCount[curLayer] < floor2roomCount[nextLayer])//���ﰢ
            {
                if (floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().paretRooms.Count < 1)
                {
                    switch (q)
                    {
                        case 1:
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().paretRooms.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                            floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).GetComponent<Room>().childRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                            createline--;
                            break;
                        case 2:
                            if (floor_List[smallLayer].transform.childCount == 2)
                            {
                                if (i == 4)
                                {
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().paretRooms.Add(floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).gameObject);
                                    floor_List[smallLayer].transform.GetChild(floor_List[smallLayer].transform.childCount - 1).GetComponent<Room>().childRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                                    createline--;
                                }
                                else
                                {
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(i / 2).gameObject);
                                    floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().paretRooms.Add(floor_List[smallLayer].transform.GetChild(i / 2).gameObject);
                                    floor_List[smallLayer].transform.GetChild(i / 2).GetComponent<Room>().childRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                                    createline--;
                                }

                            }
                            else if (floor_List[smallLayer].transform.childCount == 3)
                            {
                                floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(i / 2).gameObject);
                                floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().paretRooms.Add(floor_List[smallLayer].transform.GetChild(i / 2).gameObject);
                                floor_List[smallLayer].transform.GetChild(i / 2).GetComponent<Room>().childRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                                createline--;
                            }
                            break;
                        case 3:
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<LineScript>().target.Add(floor_List[smallLayer].transform.GetChild(i / 3).gameObject);
                            floor_List[largeLayer].transform.GetChild(i).GetComponent<Room>().paretRooms.Add(floor_List[smallLayer].transform.GetChild(i / 3).gameObject);
                            floor_List[smallLayer].transform.GetChild(i / 3).GetComponent<Room>().childRooms.Add(floor_List[largeLayer].transform.GetChild(i).gameObject);
                            createline--;
                            break;
                    }

                }
            }
        }
        Debug.Log(createline);
    }
}