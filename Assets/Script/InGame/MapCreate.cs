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
    static int floor = 16;//0�����̶� 16������ 1���ΰ����� ��������
    public GameObject room_p;
    public GameObject floor_p;
    public List<GameObject> floor_List;
    public GameObject roomObj_Parents;
    public GameObject floorObj_Parents;

    public MapTree mapTree;

    public int createRoomCount = 0;
    public List<int> roomTypeRand = new List<int>();
    
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
        
    }
    #region RoomCreate
    void SetFloorObject()
    {
        for(int i = 0; i < floor; i++)
        {
            floor_List.Add(null);
        }
        for (int i = floor-1; i >= 0; i--)//��������� ���� �ε����� ������
        {
            GameObject floorObj = Instantiate(floor_p, floorObj_Parents.transform);
            floorObj.name = "Floor[" + i + "]";
            floor_List[i] = floorObj;
            
        }
        GameObject roomObj = Instantiate(room_p, floor_List[15].transform);
        MapNode temp = roomObj.GetComponent<Room>().node;
        temp.roomType = ROOMVALUE.BOSS;
        temp.roomNum = createRoomCount;
        temp.roomName = "[" + (floor - 1) + "]" + "[0]" + temp.roomType.ToString();
        temp.children = new List<MapNode>();
        temp.floor = floor - 1;
        temp.roomObj = roomObj;
        roomObj.name = temp.roomName;
        mapTree.root = new MapNode(temp);
        createRoomCount++;
        SetRoomObject(temp);
    }
    void SetRoomObject(MapNode node)//������ ����
    {
        //���� ��屸��
        //�ֻ��� ���� ����(���������)
        //�����γ������½����� �����
        //���ϳ����鶧���� �̽�ũ��Ʈ�� ����
        if (node.floor == 0||node == null)
        {
            return;
        }
        List<MapNode> childNode = new List<MapNode>();
        int roomCount = CreateSeed.Instance.RandNum(2, 7);
        List<GameObject> roomObj = new List<GameObject>();
        
        switch (node.floor - 1)//�ڽĳ�屸��
        {
            case 14:
                for(int i = 0; i < roomCount; i++)
                {
                    roomObj.Add(Instantiate(room_p, floor_List[node.floor - 1].transform));
                    childNode.Add(roomObj[i].GetComponent<Room>().node);
                    childNode[i].roomType = ROOMVALUE.REST;
                    childNode[i].roomNum = createRoomCount;
                    childNode[i].roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode[i].roomType.ToString();
                    childNode[i].children = new List<MapNode>();
                    childNode[i].floor = node.floor - 1;
                    childNode[i].roomObj = roomObj[i];
                    roomObj[i].name = childNode[i].roomName;
                    node.children.Add(childNode[i]);
                    createRoomCount++;
                    SetRoomObject(childNode[i]);
                }
                break;
            case 8:
                for (int i = 0; i < roomCount; i++)
                {
                    roomObj.Add(Instantiate(room_p, floor_List[node.floor - 1].transform));
                    childNode.Add(roomObj[i].GetComponent<Room>().node);
                    childNode[i].roomType = ROOMVALUE.TREASURE;
                    childNode[i].roomNum = createRoomCount;
                    childNode[i].roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode[i].roomType.ToString();
                    childNode[i].children = new List<MapNode>();
                    childNode[i].floor = node.floor - 1;
                    childNode[i].roomObj = roomObj[i];
                    roomObj[i].name = childNode[i].roomName;
                    node.children.Add(childNode[i]);
                    createRoomCount++;
                    SetRoomObject(childNode[i]);
                }
                break;
            case 0:
                for (int i = 0; i < roomCount; i++)
                {
                    roomObj.Add(Instantiate(room_p, floor_List[node.floor - 1].transform));
                    childNode.Add(roomObj[i].GetComponent<Room>().node);
                    childNode[i].roomType = ROOMVALUE.NOMAL;
                    childNode[i].roomNum = createRoomCount;
                    childNode[i].roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode[i].roomType.ToString();
                    childNode[i].children = new List<MapNode>();
                    childNode[i].floor = node.floor - 1;
                    childNode[i].roomObj = roomObj[i];
                    roomObj[i].name = childNode[i].roomName;
                    node.children.Add(childNode[i]);
                    createRoomCount++;
                    SetRoomObject(childNode[i]);
                }
                break;
            default:
                for (int i = 0; i < roomCount; i++)
                {
                    roomObj.Add(Instantiate(room_p, floor_List[node.floor - 1].transform));
                    childNode.Add(roomObj[i].GetComponent<Room>().node);
                    childNode[i].roomType = (ROOMVALUE)CreateSeed.Instance.RandNum(roomTypeRand);
                    childNode[i].roomNum = createRoomCount;
                    childNode[i].roomName = "[" + (node.floor - 1) + "]" + "[" + i + "]" + childNode[i].roomType.ToString();
                    childNode[i].children = new List<MapNode>();
                    childNode[i].floor = node.floor - 1;
                    childNode[i].roomObj = roomObj[i];
                    roomObj[i].name = childNode[i].roomName;
                    node.children.Add(childNode[i]);
                    createRoomCount++;
                    SetRoomObject(childNode[i]);
                }
                break;
        }
        
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
