using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreate : MonoBehaviour
{
    //�������� 8���� ���� ���� ���� �޽� �Ϲ��� ����Ʈ ���� ���
    //0.���, 1.�Ϲ���, 2.����Ʈ, 3.�޽�, 4.����, 5.����, 6.����, 7.����
    //���������Ǵ°��� ������ ������ 6����
    //0���� �Ϲ������� ����, 14���� �޽����� ����, 15���� ���������ΰ��� 1���ΰ���
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
                    map[i, j] = rand.Next(2);//0���� ���� �Ϲ�����������
                }
            }
            else if (i == 14)
            {
                for (int j = 0; j < 6; j++)
                {
                    map[i, j] = 3;//14���� �޽ĸ���������
                }
            }
            else if(i == 15)
            {
                map[i, 3] = 7;//15���� ���� �Ѱ�����������
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    map[i, j] = rand.Next(6) + 1;//8������ ������ �������� 1~6����
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
