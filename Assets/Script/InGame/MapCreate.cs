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
    public GameObject roomObj_Parents;
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
    }
    #region RoomCreate
    void SetFloorObject()
    {
        
        for (int i = 0; i < floor; i++)
        {
            floor_List.Add(null);
        }
        for (int i = floor-1; i >= 0; i--)//보스방부터 시작 인덱스도 역순임
        {
            GameObject floorObj = Instantiate(floor_p, floorObj_Parents.transform);
            floorObj.name = "Floor[" + i + "]";
            floor_List[i] = floorObj;
        }
    }
    void RootCreate()
    {
        GameObject roomObj = Instantiate(room_p, floor_List[floor - 1].transform);
        MapNode temp = new MapNode();
        temp.roomType = ROOMVALUE.BOSS;
        temp.roomNum = createRoomCount;
        temp.roomName = "[" + createRoomCount + "]" + temp.roomType.ToString();
        
        temp.floor = floor - 1;
        temp.roomObj = roomObj;
        temp.children = new List<MapNode>();
        for (int i = 0; i < floor2roomCount[temp.floor - 1]; i++)
        {
            temp.children.Add(new MapNode());
        }
        mapTree.root = temp;
        roomObj.name = temp.roomName;
        roomObj.GetComponent<Room>().node = mapTree.root;
        room_List.Add(roomObj);
        createRoomCount++;
        for (int i = 0; i < floor2roomCount[mapTree.root.floor - 1]; i++)//하위 오브젝트만큼반복
        {
            SetRoomObject(room_List[room_List.Count - 1], mapTree.root.children[i]);//temp.children[i]빈데이터
        }
    }
    void SetRoomObject(GameObject parentObj, MapNode childnode)//하위 오브젝트 생성
    {
        //하위오브젝트 구역
        //최상층 부터 실행(보스방부터)
        //밑으로내려가는식으로 방생성
        //방하나만들때마다 이스크립트를 실행
        MapNode pNode = parentObj.GetComponent<Room>().node;
        
        GameObject roomObj = Instantiate(room_p, floor_List[pNode.floor - 1].transform);

        switch (pNode.floor - 1)
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
        childnode.floor = pNode.floor - 1;
        childnode.children = new List<MapNode>();
        if (childnode.floor - 1 >= 0 && floor2roomCount[childnode.floor - 1] > 0)
        {
            for (int i = 0; i < floor2roomCount[childnode.floor-1]; i++)
            {
                childnode.children.Add(new MapNode());
            }
        }
        else
        {
            return;
        }
        childnode.roomObj = roomObj;
        roomObj.GetComponent<Room>().node = childnode;
        room_List.Add(roomObj);
        roomObj.name = childnode.roomName;
        createRoomCount++;
        if(childnode.floor - 1 > 0)
        {
            for (int i = 0; i < floor2roomCount[childnode.floor - 1]; i++)//하위의 하위오브젝트만큼반복
            {
                SetRoomObject(room_List[room_List.Count - 1], childnode.children[i]);//ccNode[i]빈데이터
            }
        }
        else
        {
            return;
        }
        
    }
    
    #endregion

    #region CreateLine V1
    void CreateLine(int layer)//한층마다 실행 한층마다 생성되는 선의 갯수를 리턴
    {
        #region 선생성할 갯수 설정
        #region 설명
        //1.다음 층으로 넘어갈때 쓸수있는 선의갯수 최대 6개
        //2.생성된 다음방은 어느방에서부터든 한방은 갈수있어야한다
        //3. 1~3, 4~5 i층 j번방 i+1층 j-1~j+1까지 가능 한방에 최대 3개 최소1개의 선이생김
        //4.방은 2개에서 6개사이로 생성됨

        //생성하는방법
        //1.현재층의 방의 개수를 저장하고 다음방의 갯수를 찾는다
        // 
        //  최소 = 현재방 다음방의 최대값
        //  최대 = 최소의 방의 갯수에따라 달라짐
        // ex) 현재방과 다음방의 갯수가 같을때 2n-1 (a-b = 0 2n-1)
        // 2 2 = 3, 3 3 = 5, 4 4 = 7
        // ex) 현재방과 다음방의 갯수의 차가 1일때 2n
        // 2 3 = 4, 3 4 = 6, 4 5 = 8 
        // ex) 현재방과 다음방의 갯수의 차가 2일때 2n + 1
        // 2 4 = 5 3 5 = 7, 4 6 = 9
        // 선의 최소생성개수 = 현재방과 다음방의 최대값
        // 선의 최대 생성개수 = (현재방과 다음방중의 최대값)*2-1 + (현재방과 다음방의 갯수의 차)
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
    void SetLine(int layer, int createdLine)//갯수가 설정되면 그갯수가지고 층과층사이를 연결되도록배치
    {
        /*
        1.현재층과 다음층의 갯수가 같을경우
            같은번수의 방끼리 연결 -> 남아있는 선갯수 확인 -> 남아있는 선갯수가 있으면 현제층부터 선을 추가로 연결할 방을 랜덤으로설정하고 한번더 연결시도(n-1 or n+1번방만 가능)
        2.현재 층과 다음층의 갯수가 다를경우
            현재층과 다음층의 양쪽끝방끼리 이어주기 -> 남은 현재방과 다음방의 갯수를 체크
            ->

         */
    }
    #endregion

    #region CreateLine V2
    void CreateLine_V2(int layer)
    {
        /*
         * 현재층의 갯수만큼만 선생성
         * 현재층과 다음충중 높은방의갯수를 가진층을 낮은방의갯수를 가진층으로 나눈다 
         */

    }

    #endregion
}
