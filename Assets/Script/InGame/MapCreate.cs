using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreate : MonoBehaviour
{
    //방의종류 8가지 미지 상인 보물 휴식 일반적 엘리트 보스 빈방
    //0.빈방, 1.일반적, 2.엘리트, 3.휴식, 4.상인, 5.보물, 6.미지, 7.보스
    //랜덤생성되는것은 보스를 제외한 6가지
    //0층은 일반적으로 고정, 14층은 휴식으로 고정, 15층은 보스방으로고정 1개로고정
    int[,] map = new int[16, 6];
    // Start is called before the first frame update
    void Start()
    {
        int seed = DateTime.Now.Millisecond;
        System.Random rand = new System.Random(seed);
        for(int i = 0; i < 16; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 6; j++)
                {
                    map[i, j] = rand.Next(2);//0층은 빈방과 일반적만나오게
                }
            }
            else if (i == 14)
            {
                for (int j = 0; j < 6; j++)
                {
                    map[i, j] = 3;//14층은 휴식만나오도록
                }
            }
            else if(i == 15)
            {
                map[i, 3] = 7;//15층은 보스 한곳만나오도록
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    map[i, j] = rand.Next(6) + 1;//8가지의 방목록중 랜덤방은 1~6까지
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
