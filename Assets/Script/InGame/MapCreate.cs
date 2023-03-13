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
    //0���� �Ϲ������� ����, 13���� �޽����� ����, 14���� ���������ΰ��� 1���ΰ���

    /*
     1. ���� ��������� ����
        �� ���� �����ϸ� �켱 ���� 7 x ���� 15¥�� ���ڸ� ����. �̰� �츮�� ���� �Ǵ� ���� ����.
        �� ������ (���ϵ������� ������ �̷�?? �ΰ� �ƹ�ư �� ���ڷ� ����� �ɼ� �����ִ°� �ƴ϶��)
        ���ι������� y��ǥ 1���� 15���� ��θ� �� ����.
        �� ��δ� y������ 1��ŭ �����Ҷ� x��ǥ�� -1���� 1���� �����ϰ� ���ϸ鼭 �ö�.
        �̰� �� 6�� �ݺ��� ������ ��λ� �ִ� ��� ���������� ������ ������� ����.
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
    //0���� ���� �Ϲ�����������
    //8���� ���� ����
    //14���� �޽ĸ���������
    //15���� ���� �Ѱ�����������
    //8������ ������ �������� 0~5����

    /*
     �̸� �ִ�ũ���� ��(��)�� ����� �װ͵��� ������Ʈ Ǯ��������
     
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

        // ��� ������ ��ü�� ���� ��� ���� ���� / ���׾ƴ��̻� �������δ� �ȿõ�
        GameObject newObj = Instantiate(room_p, Vector3.zero, Quaternion.identity);
        newObj.SetActive(true);
        newObj.transform.SetParent(roomObj_Parents.transform);
        newObj.name = "Room[" + createCount_Op / 7 + "][" + createCount_Op % 7 + "]";
        createCount_Op++;
        objectPool.Add(newObj);
        return newObj;
    }

    // ����� �Ϸ�� ��ü�� ������Ʈ Ǯ�� ��ȯ�ϴ� �Լ�
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
