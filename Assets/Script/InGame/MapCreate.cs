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
    static public int seed = Environment.TickCount;
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
        
        //Create_RoomObj(0, 0);
        SetObjectPool();

        UseObjectPool();
    }
    void UseObjectPool()
    {
        GameObject room = GetObjectFromPool();
        room.SetActive(true);
    }
    int RandNum(int min, int max)
    {
        System.Random random = new System.Random(seed);
        int randnum = random.Next(max - min) + min;
        return randnum;
    }
    int[] RandNumNotSame(int min, int max, int count)
    {
        int[] randarr = new int[count];
        System.Random random = new System.Random(seed);
        int randnum = 0;
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                randarr[i] = random.Next(max - min) + min;
            }
            else if (i > 0)
            {
                
            }
            
        }
        
        return randarr;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetObjectPool()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(room_p, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            obj.transform.SetParent(roomObj_Parents.transform);
            obj.name = "Room[" + createCount_Op / 7 + "][" + createCount_Op % 7 + "]";
            createCount_Op++;
            objectPool.Add(obj);
        }
    }
    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 사용 가능한 객체가 없을 경우 새로 생성 / 버그아닌이상 이쪽으로는 안올듯
        GameObject newObj = Instantiate(room_p, Vector3.zero, Quaternion.identity);
        newObj.SetActive(true);
        newObj.transform.SetParent(roomObj_Parents.transform);
        newObj.name = "Room[" + createCount_Op / 7 + "][" + createCount_Op % 7 + "]";
        createCount_Op++;
        objectPool.Add(newObj);
        return newObj;
    }

    // 사용이 완료된 객체를 오브젝트 풀로 반환하는 함수
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
