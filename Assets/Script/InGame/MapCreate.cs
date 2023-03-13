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
        //1.��� �游 ��Ȱ��ȭ����... ������ 1~5������ ��Ȱ��ȭ ����
        //2.�����ġ�� ���� ��Ȱ��ȭ �Ұ���... �ߺ��ȵǰԼ���
        //3.�������� ���Լ��� ����
        UnityEngine.Random.InitState(CreateSeed.Instance.randomSeed);
        int randomNumber = UnityEngine.Random.Range(1, 6);//�ּ�,�ּ�+�ִ�
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
        // ����Ʈ�� ���� ������ 0�� ��ȯ
        if (availableValues.Count == 0)
        {
            return -1;
        }

        // ����Ʈ���� ������ �ε����� �����ϰ� �� ���� ��ȯ
        int randomIndex = UnityEngine.Random.Range(0, availableValues.Count);
        int randomValue = availableValues[randomIndex];

        // ����Ʈ���� �ش� ���� �����ϰ� ��ȯ
        availableValues.RemoveAt(randomIndex);
        return randomValue;
    }
}
