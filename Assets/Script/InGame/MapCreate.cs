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
    //0층은 일반적으로 고정, 14층은 휴식으로 고정, 15층은 보스방으로고정 1개로고정

    /*
     1. 맵이 만들어지는 과정
        각 막에 진입하면 우선 가로 7 x 세로 15짜리 격자를 만듬. 이게 우리가 돌게 되는 맵의 뼈대.
        그 다음에 (일일도전에서 유일한 미래?? 인가 아무튼 길 일자로 만드는 옵션 켜져있는게 아니라면)
        세로방향으로 y좌표 1부터 15까지 경로를 쭉 그음.
        각 경로는 y축으로 1만큼 증가할때 x좌표는 -1부터 1까지 랜덤하게 변하면서 올라감.
        이걸 총 6번 반복한 다음에 경로상에 있는 모든 격자점들을 모은게 결과적인 맵임.
     */
    static int row = 16;
    static int col = 6;
    int[,] map = new int[row, col];
    public GameObject room_Parent;

    void Create_Frame()
    {
        int seed = DateTime.Now.Millisecond;
        System.Random rand = new System.Random(seed);
        for (int i = 0; i < row; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = rand.Next(2);//0층은 빈방과 일반적만나오게
                }
            }
            else if (i == 8)
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = 5;//8층은 보물 고정
                }
            }
            else if (i == 14)
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = 3;//14층은 휴식만나오도록
                }
            }
            else if (i == 15)
            {
                map[i, 3] = 7;//15층은 보스 한곳만나오도록
            }
            else
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = rand.Next(6);//8가지의 방목록중 랜덤방은 0~5까지
                }
            }

        }
    }
    void Create_RoomObj(int row, int col)
    {
        GameObject room_Btn = new GameObject("Room[" + row + "][" + col + "]");
        room_Btn.AddComponent<Button>();
        room_Btn.transform.SetParent(room_Parent.transform);
        room_Btn.GetComponent<Transform>().position = new Vector2(room_Parent.transform.position.x, room_Parent.transform.position.y);
        //GameObject textP = GameObject.Find("Room");
        //GameObject textC = textP.transform.GetChild(0).gameObject;
        //Destroy(textC);
    }
    void Start()
    {
        //Create_Frame();
        Create_RoomObj(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
