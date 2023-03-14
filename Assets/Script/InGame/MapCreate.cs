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
    public GameObject room_p;
    public GameObject floor_p;
    public int poolSize = row * col;
    public List<List<GameObject>> room_List;
    public List<GameObject> floor_List;
    public GameObject roomObj_Parents;
    public GameObject floorObj_Parents;
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
        SetFloorObject();
        room_List = new List<List<GameObject>>();
        for (int i = 0; i < row; i++)
        {
            room_List.Add(new List<GameObject>());
            SetRoomObject(i);
        }
    }
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
    void SetRoomObject(int layer)//������ ����
    {
        //1.��� ���� ���� �Ұ��� ������ �ּ�2�� ~ �ִ�6������ Ȱ��ȭ ����
        //2.�����ġ�� ���� ��Ȱ��ȭ �Ұ���... �ߺ��ȵǰԼ���
        //3.�������� ���Լ��� ����
        
        int randomNumber = CreateSeed.Instance.RandNum(1, 6);//�ּ�,�ּ�+�ִ�
        List<int> ableNum = new List<int>();//�ߺ��ȵǰ� ���������̴� ����Ʈ
        List<int> delRoomNum = new List<int>();//�����Ǵ� ���ȣ ����Ʈ
        for(int i = 0; i < col; i++)
        {
            GameObject roomObj = Instantiate(room_p, Vector3.zero, Quaternion.identity);//�����
            roomObj.transform.SetParent(floor_List[layer].transform);//������Ȱ� �θ���
            roomObj.name = "Room[" + layer + "][" + i + "]";//���̸� ����
            room_List[layer].Add(roomObj);//�����ȹ� ����Ʈ�� ����
            ableNum.Add(i);//�ߺ��ȵǰ� �������� �̱����� 0~6���� ����Ʈ�� ����
        }
        for(int i = 0; i < randomNumber; i++)
        {
            delRoomNum.Add(GetRandomValue(ableNum));
        }
        for(int i = 0; i < delRoomNum.Count; i++)
        {
            room_List[layer][delRoomNum[i]].SetActive(false);
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
        int randomIndex = CreateSeed.Instance.RandNum(0, availableValues.Count);
        int randomValue = availableValues[randomIndex];

        // ����Ʈ���� �ش� ���� �����ϰ� ��ȯ
        availableValues.RemoveAt(randomIndex);
        return randomValue;
    }
}
