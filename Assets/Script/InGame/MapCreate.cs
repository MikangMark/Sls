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
    public GameObject roomObj_Parents;
    public GameObject floorObj_Parents;

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
        for(int i = 0; i < 8; i++)
        {
            if(i==0|| i == 6|| i == 7)
            {
                continue;
            }
            roomTypeRand.Add(i);
        }
        mapTree = new MapTree();
        SetFloorObject();
        floor2roomCount = new List<int>();
        for(int i = 0; i < floor; i++)
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
    }
    #region RoomCreate
    void SetFloorObject()
    {
        
        for (int i = 0; i < floor; i++)
        {
            floor_List.Add(null);
        }
        for (int i = floor-1; i >= 0; i--)//��������� ���� �ε����� ������
        {
            GameObject floorObj = Instantiate(floor_p, floorObj_Parents.transform);
            floorObj.name = "Floor[" + i + "]";
            floor_List[i] = floorObj;
        }
    }
    void RootCreate()
    {
        GameObject roomObj = Instantiate(room_p, floor_List[floor - 1].transform);
        MapNode root = roomObj.GetComponent<Room>().node;
        List<MapNode> childTemp = new List<MapNode>();
        for(int i = 0; i < floor2roomCount[14]; i++)
        {
            childTemp.Add(null);
        }
        root.roomType = ROOMVALUE.BOSS;
        root.roomNum = createRoomCount;
        root.roomName = "[" + createRoomCount + "]" + root.roomType.ToString();
        root.children = childTemp;
        root.floor = floor - 1;
        root.roomObj = roomObj;
        roomObj.name = root.roomName;
        mapTree.root = root;
        createRoomCount++;
        for (int i = 0; i < floor2roomCount[root.floor - 1]; i++)//���� ������Ʈ��ŭ�ݺ�
        {
            SetRoomObject(root, childTemp[i]);//childTemp[i]������
        }
    }
    void SetRoomObject(MapNode parentnode, MapNode childnode)//���� ������Ʈ ����
    {
        //����������Ʈ ����
        //�ֻ��� ���� ����(���������)
        //�����γ������½����� �����
        //���ϳ����鶧���� �̽�ũ��Ʈ�� ����
        if (parentnode.floor - 1 < 0)
        {
            return;
        }
        GameObject roomObj = Instantiate(room_p, floor_List[parentnode.floor - 1].transform);
        childnode = roomObj.GetComponent<Room>().node;
        List<MapNode> ccNode = new List<MapNode>();
        if(parentnode.floor - 2 >= 0)
        {
            for (int i = 0; i < floor2roomCount[parentnode.floor - 2]; i++)
            {
                ccNode.Add(null);
            }
        }
        
        switch (parentnode.floor - 1)
        {
            case 14:
                childnode.roomType = ROOMVALUE.REST;
                break;
            case 8:
                childnode.roomType = ROOMVALUE.TREASURE;
                break;
            case 0:
                childnode.roomType = ROOMVALUE.NOMAL;
                break;
            default:
                childnode.roomType = (ROOMVALUE)CreateSeed.Instance.RandNum(roomTypeRand);
                break;
        }
        childnode.roomNum = createRoomCount;
        childnode.roomName = "[" + createRoomCount + "]" + childnode.roomType.ToString();
        childnode.children = ccNode;
        childnode.floor = parentnode.floor - 1;
        childnode.roomObj = roomObj;
        roomObj.name = childnode.roomName;
        createRoomCount++;
        for (int i = 0; i < floor2roomCount[childnode.floor - 1]; i++)//������ ����������Ʈ��ŭ�ݺ�
        {
            SetRoomObject(childnode, ccNode[i]);//ccNode[i]������
        }
        /*
        MapNode ccNode;
        
        GameObject roomObj;
        
        switch (node.floor - 1)//�ڽĳ�屸��
        {
            case 14:
                for(int i = 0; i < roomCount; i++)
                {
                    roomObj = Instantiate(room_p, floor_List[node.floor - 1].transform);
                    childNode = roomObj.GetComponent<Room>().node;
                    childNode.roomType = ROOMVALUE.REST;
                    childNode.roomNum = createRoomCount;
                    childNode.roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode.roomType.ToString();
                    childNode.children = new List<MapNode>();
                    childNode.floor = node.floor - 1;
                    childNode.roomObj = roomObj;
                    roomObj.name = childNode.roomName;
                    node.children.Add(childNode);
                    createRoomCount++;
                    if (childNode.floor > 0)
                    {
                        SetRoomObject(childNode);
                    }
                }
                break;
            case 8:
                for (int i = 0; i < roomCount; i++)
                {
                    roomObj = Instantiate(room_p, floor_List[node.floor - 1].transform);
                    childNode = roomObj.GetComponent<Room>().node;
                    childNode.roomType = ROOMVALUE.TREASURE;
                    childNode.roomNum = createRoomCount;
                    childNode.roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode.roomType.ToString();
                    childNode.children = new List<MapNode>();
                    childNode.floor = node.floor - 1;
                    childNode.roomObj = roomObj;
                    roomObj.name = childNode.roomName;
                    node.children.Add(childNode);
                    createRoomCount++;
                    if (childNode.floor > 0)
                    {
                        SetRoomObject(childNode);
                    }
                }
                break;
            case 0:
                for (int i = 0; i < roomCount; i++)
                {
                    roomObj = Instantiate(room_p, floor_List[node.floor - 1].transform);
                    childNode = roomObj.GetComponent<Room>().node;
                    childNode.roomType = ROOMVALUE.NOMAL;
                    childNode.roomNum = createRoomCount;
                    childNode.roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode.roomType.ToString();
                    childNode.children = new List<MapNode>();
                    childNode.floor = node.floor - 1;
                    childNode.roomObj = roomObj;
                    roomObj.name = childNode.roomName;
                    node.children.Add(childNode);
                    createRoomCount++;
                    if (childNode.floor > 0)
                    {
                        SetRoomObject(childNode);
                    }
                }
                break;
            default:
                for (int i = 0; i < roomCount; i++)
                {
                    roomObj = Instantiate(room_p, floor_List[node.floor - 1].transform);
                    childNode = roomObj.GetComponent<Room>().node;
                    childNode.roomType = (ROOMVALUE)CreateSeed.Instance.RandNum(roomTypeRand);
                    childNode.roomNum = createRoomCount;
                    childNode.roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode.roomType.ToString();
                    childNode.children = new List<MapNode>();
                    childNode.floor = node.floor - 1;
                    childNode.roomObj = roomObj;
                    roomObj.name = childNode.roomName;
                    node.children.Add(childNode);
                    createRoomCount++;
                    if (childNode.floor > 0)
                    {
                        SetRoomObject(childNode);
                    }
                }
                break;
        }
        */
    }
    
    #endregion

    #region CreateLine V1
    void CreateLine(int layer)//�������� ���� �������� �����Ǵ� ���� ������ ����
    {
        #region �������� ���� ����
        #region ����
        //1.���� ������ �Ѿ�� �����ִ� ���ǰ��� �ִ� 6��
        //2.������ �������� ����濡�����͵� �ѹ��� �����־���Ѵ�
        //3. 1~3, 4~5 i�� j���� i+1�� j-1~j+1���� ���� �ѹ濡 �ִ� 3�� �ּ�1���� ���̻���
        //4.���� 2������ 6�����̷� ������

        //�����ϴ¹��
        //1.�������� ���� ������ �����ϰ� �������� ������ ã�´�
        // 
        //  �ּ� = ����� �������� �ִ밪
        //  �ִ� = �ּ��� ���� ���������� �޶���
        // ex) ������ �������� ������ ������ 2n-1 (a-b = 0 2n-1)
        // 2 2 = 3, 3 3 = 5, 4 4 = 7
        // ex) ������ �������� ������ ���� 1�϶� 2n
        // 2 3 = 4, 3 4 = 6, 4 5 = 8 
        // ex) ������ �������� ������ ���� 2�϶� 2n + 1
        // 2 4 = 5 3 5 = 7, 4 6 = 9
        // ���� �ּһ������� = ������ �������� �ִ밪
        // ���� �ִ� �������� = (������ ���������� �ִ밪)*2-1 + (������ �������� ������ ��)
        #endregion
        
        /*int lineCount;
        int lineMin;
        int lineMax;
        int nowMnext = nowFloor_Count - nextFloor_Count;//now - next = nowMnext

        for(int i = 0;i< room_List[layer].Count; i++)
        {
            if (room_List[layer][i].activeSelf == true)
            {
                nowFloor_Count++;
            }
        }
        for (int i = 0; i < room_List[layer+1].Count; i++)
        {
            if (room_List[layer + 1][i].activeSelf == true)
            {
                nextFloor_Count++;
            }
        }

        if (nowMnext >= 0)
        {
            lineMin = nowFloor_Count;
            lineMax = nextFloor_Count * 2 - 1 + nowMnext;
        }
        else
        {
            lineMin = nextFloor_Count;
            lineMax = nowFloor_Count * 2 - 1 + (-nowMnext);
        }
        lineCount = CreateSeed.Instance.RandNum(lineMin, lineMax);
        SetLine(layer, lineCount);*/
        #endregion
    }
    void SetLine(int layer, int createdLine)//������ �����Ǹ� �װ��������� ���������̸� ����ǵ��Ϲ�ġ
    {
        /*
        1.�������� �������� ������ �������
            ���������� �波�� ���� -> �����ִ� ������ Ȯ�� -> �����ִ� �������� ������ ���������� ���� �߰��� ������ ���� �������μ����ϰ� �ѹ��� ����õ�(n-1 or n+1���游 ����)
        2.���� ���� �������� ������ �ٸ����
            �������� �������� ���ʳ��波�� �̾��ֱ� -> ���� ������ �������� ������ üũ
            ->

         */
    }
    #endregion

    #region CreateLine V2
    void CreateLine_V2(int layer)
    {
        /*
         * �������� ������ŭ�� ������
         * �������� �������� �������ǰ����� �������� �������ǰ����� ���������� ������ 
         */

    }

    #endregion
}
