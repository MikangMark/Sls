using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ROOMVALUE = MapNode.ROOMVALUE;


public class MapCreate : MonoBehaviour
{

    //방의종류 8가지 미지 상인 보물 휴식 일반적 엘리트 보스 빈방
    //0.빈방, 1.일반적, 2.엘리트, 3.휴식, 4.상인, 5.미지, 6.보물, 7.보스
    //랜덤생성되는것은 보스를 제외한 6가지
    //0층은 일반적으로 고정, 13층은 휴식으로 고정, 14층은 보스방으로고정 1개로고정

    /*
     * 처음 보상받고 넘어가는방 보이지않는 0번방
     * 보스방 미리 랜덤설정되어있는방과 따로 존재하는 방
     1. 맵이 만들어지는 과정
        각 막에 진입하면 우선 가로 7 x 세로 15짜리 격자를 만듬. 이게 우리가 돌게 되는 맵의 뼈대.
        그 다음에 (일일도전에서 유일한 미래?? 인가 아무튼 길 일자로 만드는 옵션 켜져있는게 아니라면)
        세로방향으로 y좌표 1부터 15까지 경로를 쭉 그음.
        각 경로는 y축으로 1만큼 증가할때 x좌표는 -1부터 1까지 랜덤하게 변하면서 올라감.
        이걸 총 6번 반복한 다음에 경로상에 있는 모든 격자점들을 모은게 결과적인 맵임.
     */
    static int floor = 16;//0번방이랑 15번방은 1개로고정및 종류고정
    public GameObject room_p;
    public GameObject floor_p;
    public List<GameObject> floor_List;
    public GameObject floorObj_Parents;
    public List<GameObject> room_List;
    public MapTree mapTree;
    public int createRoomCount = 0;
    public List<int> roomTypeRand = new List<int>();
    public List<int> floor2roomCount;

    //0층은 빈방과 일반적만나오게
    //8층은 보물 고정
    //14층은 휴식만나오도록
    //15층은 보스 한곳만나오도록
    //8가지의 방목록중 랜덤방은 0~5까지
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
        for (int i = floor - 1; i >= 0; i--)//보스방부터 시작 인덱스도 역순임
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
    void SetRoomObject(int i, int j)//하위 오브젝트 생성
    {
        //하위오브젝트 구역
        //최상층 부터 실행(보스방부터)
        //밑으로내려가는식으로 방생성
        //방하나만들때마다 이스크립트를 실행
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
        방은 이미 생성되어있다
        층과 층사이의 생성되는 선의갯수는 최소3개에서 6개까지이다
        층과 층에서 방의 갯수가 많은쪽의 방의갯수가 3이상일경우 선의 최소 갯수는 방개수가많은 층의 방개수가 된다
        선들은 서로 교차되어서는 안된다
        한방에서 다음방으로 연결되는 선의갯수는 3개까지 가능하다
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

            if (floor2roomCount[curLayer] > floor2roomCount[nextLayer])//역삼각
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
            else if (floor2roomCount[curLayer] < floor2roomCount[nextLayer])//정삼각
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