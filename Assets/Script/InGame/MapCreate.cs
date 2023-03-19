using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
    static int row = 15;
    static int col = 7;
    public GameObject room_p;
    public GameObject floor_p;
    public int poolSize = row * col;
    public List<List<GameObject>> room_List;
    public List<GameObject> floor_List;
    public GameObject roomObj_Parents;
    public GameObject floorObj_Parents;

    int nowFloor_Count = 0;
    int nextFloor_Count = 0;

    //0층은 빈방과 일반적만나오게
    //8층은 보물 고정
    //14층은 휴식만나오도록
    //15층은 보스 한곳만나오도록
    //8가지의 방목록중 랜덤방은 0~5까지
    void Start()
    {
        SetFloorObject();
        room_List = new List<List<GameObject>>();
        for (int i = 0; i < row; i++)
        {
            room_List.Add(new List<GameObject>());
            SetRoomObject(i);
        }
    }
    #region RoomCreate
    void SetFloorObject()
    {
        for (int i = 0; i < row; i++)
        {
            GameObject floorObj = Instantiate(floor_p, Vector3.zero, Quaternion.identity);
            floorObj.transform.SetParent(floorObj_Parents.transform);
            floorObj.name = "Floor[" + i + "]";
            floor_List.Add(floorObj);
        }
    }
    void SetRoomObject(int layer)//층마다 실행
    {
        //1.몇개의 방을 생성 할건지 랜덤값 최소2개 ~ 최대6개까지 활성화 가능
        //2.어느위치의 방을 비활성화 할건지... 중복안되게설정
        //3.한층마다 이함수를 실행
        
        int randomNumber = CreateSeed.Instance.RandNum(1, 6);//최소,최소+최대
        List<int> ableNum = new List<int>();//중복안되게 랜덤값을뽑는 리스트
        List<int> delRoomNum = new List<int>();//삭제되는 방번호 리스트
        int num = 0;
        for (int i = 0; i < col; i++)
        {
            GameObject roomObj = Instantiate(room_p, Vector3.zero, Quaternion.identity);//방생성
            roomObj.transform.SetParent(floor_List[layer].transform);//방생성된것 부모설정
            roomObj.name = "Room[" + layer + "][" + i + "]";//방이름 설정
            room_List[layer].Add(roomObj);//생성된방 리스트에 저장
            ableNum.Add(i);//중복안되게 랜덤값을 뽑기위해 0~6까지 리스트에 저장
        }
        for(int i = 0; i < randomNumber; i++)
        {
            delRoomNum.Add(GetRandomValue(ableNum));
        }
        for(int i = 0; i < delRoomNum.Count; i++)
        {
            room_List[layer][delRoomNum[i]].SetActive(false);
        }
        for (int i = 0; i < room_List[layer].Count; i++)
        {
            if (room_List[layer][i].activeSelf)
            {
                room_List[layer][i].name = "Room[" + layer + "][" + num + "]";//방이름 설정
                num++;
            }
            else
            {
                room_List[layer][i].name = "Room[" + layer + "][" + (col - num) + "]";
            }
            
        }
    }
    private int GetRandomValue(List<int> availableValues)//중복되지않는 랜덤설정
    {
        // 리스트에 값이 없으면 0을 반환
        if (availableValues.Count == 0)
        {
            return -1;
        }

        // 리스트에서 랜덤한 인덱스를 선택하고 그 값을 반환
        int randomIndex = CreateSeed.Instance.RandNum(0, availableValues.Count);
        int randomValue = availableValues[randomIndex];

        // 리스트에서 해당 값을 제거하고 반환
        availableValues.RemoveAt(randomIndex);
        return randomValue;
    }
    #endregion
    
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
        
        int lineCount;
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
        SetLine(layer, lineCount);
        #endregion
    }
    void SetLine(int layer, int createdLine)//갯수가 설정되면 그갯수가지고 층과층사이를 연결되도록배치
    {
        /*
        1.현재층과 다음층의 갯수가 같을경우
            같은번수의 방끼리 연결 -> 남아있는 선갯수 확인 -> 남아있는 선갯수가 있으면 현제층부터 선을 추가로 연결할 방을 랜덤으로설정하고 한번더 연결시도(n-1 or n+1번방만 가능)
        2.현재층과 다음층의 차가 짝수일경우
            
        3.현재층과 다음층의 차가 홀수일경우
         */
    }
}
