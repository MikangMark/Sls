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
     1. 맵이 만들어지는 과정
        각 막에 진입하면 우선 가로 7 x 세로 15짜리 격자를 만듬. 이게 우리가 돌게 되는 맵의 뼈대.
        그 다음에 (일일도전에서 유일한 미래?? 인가 아무튼 길 일자로 만드는 옵션 켜져있는게 아니라면)
        세로방향으로 y좌표 1부터 15까지 경로를 쭉 그음.
        각 경로는 y축으로 1만큼 증가할때 x좌표는 -1부터 1까지 랜덤하게 변하면서 올라감.
        이걸 총 6번 반복한 다음에 경로상에 있는 모든 격자점들을 모은게 결과적인 맵임.
     */
    static int row = 15;
    static int col = 7;
    int createCount_Op = 0;
    int createRoomCount = 0;
    int[,] map = new int[row, col];
    public GameObject room_p;
    public int poolSize = row * col;
    public List<GameObject> objectPool;
    public GameObject roomObj_Parents;
    //0층은 빈방과 일반적만나오게
    //8층은 보물 고정
    //14층은 휴식만나오도록
    //15층은 보스 한곳만나오도록
    //8가지의 방목록중 랜덤방은 0~5까지

    /*
     미리 최대크기의 맵(방)을 만들고 그것들을 오브젝트 풀링기법사용
     
     */
    void Start()
    {
        SetObject();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetObject()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(room_p, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(roomObj_Parents.transform);
            obj.name = "Room[" + createCount_Op / 7 + "][" + createCount_Op % 7 + "]";
            createCount_Op++;
            objectPool.Add(obj);
        }
    }
    void SetUnableRoom(int layer)
    {
        //1.몇개의 방만 비활성화할지... 랜덤값 1~5개까지 비활성화 가능
        //2.어느위치의 방을 비활성화 할건지... 중복안되게설정
        //3.한층마다 이함수를 실행
        UnityEngine.Random.InitState(CreateSeed.Instance.randomSeed);
        int randomNumber = UnityEngine.Random.Range(1, 6);//최소,최소+최대
        List<int> ableNum = new List<int>();
        List<int> delRoomNum = new List<int>();
        for(int i = 0; i < 7; i++)
        {
            ableNum.Add(i);
        }
        for(int i = 0; i < randomNumber; i++)
        {
            delRoomNum.Add(GetRandomValue(ableNum));
        }
        for(int i = 0; i < delRoomNum.Count; i++)
        {
            objectPool[delRoomNum[i] + (layer * 7)].SetActive(false);
        }
    }
    private int GetRandomValue(List<int> availableValues)
    {
        // 리스트에 값이 없으면 0을 반환
        if (availableValues.Count == 0)
        {
            return -1;
        }

        // 리스트에서 랜덤한 인덱스를 선택하고 그 값을 반환
        int randomIndex = UnityEngine.Random.Range(0, availableValues.Count);
        int randomValue = availableValues[randomIndex];

        // 리스트에서 해당 값을 제거하고 반환
        availableValues.RemoveAt(randomIndex);
        return randomValue;
    }
}
