using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MapCreate : MonoBehaviour
{
    //�������� 8���� ���� ���� ���� �޽� �Ϲ��� ����Ʈ ���� ���
    //0.���, 1.�Ϲ���, 2.����Ʈ, 3.�޽�, 4.����, 5.����, 6.����, 7.����
    //���������Ǵ°��� ������ ������ 6����
    //0���� �Ϲ������� ����, 14���� �޽����� ����, 15���� ���������ΰ��� 1���ΰ���

    /*
     1. ���� ��������� ����
        �� ���� �����ϸ� �켱 ���� 7 x ���� 15¥�� ���ڸ� ����. �̰� �츮�� ���� �Ǵ� ���� ����.
        �� ������ (���ϵ������� ������ �̷�?? �ΰ� �ƹ�ư �� ���ڷ� ����� �ɼ� �����ִ°� �ƴ϶��)
        ���ι������� y��ǥ 1���� 15���� ��θ� �� ����.
        �� ��δ� y������ 1��ŭ �����Ҷ� x��ǥ�� -1���� 1���� �����ϰ� ���ϸ鼭 �ö�.
        �̰� �� 6�� �ݺ��� ������ ��λ� �ִ� ��� ���������� ������ ������� ����.
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
                    map[i, j] = rand.Next(2);//0���� ���� �Ϲ�����������
                }
            }
            else if (i == 8)
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = 5;//8���� ���� ����
                }
            }
            else if (i == 14)
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = 3;//14���� �޽ĸ���������
                }
            }
            else if (i == 15)
            {
                map[i, 3] = 7;//15���� ���� �Ѱ�����������
            }
            else
            {
                for (int j = 0; j < col; j++)
                {
                    map[i, j] = rand.Next(6);//8������ ������ �������� 0~5����
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
