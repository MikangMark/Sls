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
     * ó�� ����ް� �Ѿ�¹� �������ʴ� 0����
     * ������ �̸� ���������Ǿ��ִ¹�� ���� �����ϴ� ��
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

    int nowFloor_Count = 0;
    int nextFloor_Count = 0;

    //0���� ���� �Ϲ�����������
    //8���� ���� ����
    //14���� �޽ĸ���������
    //15���� ���� �Ѱ�����������
    //8������ ������ �������� 0~5����
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
    void SetRoomObject(int layer)//������ ����
    {
        //1.��� ���� ���� �Ұ��� ������ �ּ�2�� ~ �ִ�6������ Ȱ��ȭ ����
        //2.�����ġ�� ���� ��Ȱ��ȭ �Ұ���... �ߺ��ȵǰԼ���
        //3.�������� ���Լ��� ����
        
        int randomNumber = CreateSeed.Instance.RandNum(1, 6);//�ּ�,�ּ�+�ִ�
        List<int> ableNum = new List<int>();//�ߺ��ȵǰ� ���������̴� ����Ʈ
        List<int> delRoomNum = new List<int>();//�����Ǵ� ���ȣ ����Ʈ
        int num = 0;
        for (int i = 0; i < col; i++)
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
        for (int i = 0; i < room_List[layer].Count; i++)
        {
            if (room_List[layer][i].activeSelf)
            {
                room_List[layer][i].name = "Room[" + layer + "][" + num + "]";//���̸� ����
                num++;
            }
            else
            {
                room_List[layer][i].name = "Room[" + layer + "][" + (col - num) + "]";
            }
            
        }
    }
    private int GetRandomValue(List<int> availableValues)//�ߺ������ʴ� ��������
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
    #endregion
    
    void CreateLine(int layer)//�������� ���� �������� �����Ǵ� ���� ������ ����
    {
        #region �������� ���� ����
        #region ����
        //1.���� ������ �Ѿ�� �����ִ� ���ǰ��� �ִ� 6��
        //2.������ �������� ����濡�����͵� �ѹ��� �����־���Ѵ�
        //3. 1~3, 4~5 i�� j���� i+1�� j-1~j+1���� ���� �ѹ濡 �ִ� 3�� �ּ�1���� ���̻���
        //4.���� 2������ 6�����̷� ������

        //�����ϴ¹��
        //1.�������� ���� ������ �����ϰ� �������� ������ ã�´�
        // 
        //  �ּ� = ����� �������� �ִ밪
        //  �ִ� = �ּ��� ���� ���������� �޶���
        // ex) ������ �������� ������ ������ 2n-1 (a-b = 0 2n-1)
        // 2 2 = 3, 3 3 = 5, 4 4 = 7
        // ex) ������ �������� ������ ���� 1�϶� 2n
        // 2 3 = 4, 3 4 = 6, 4 5 = 8 
        // ex) ������ �������� ������ ���� 2�϶� 2n + 1
        // 2 4 = 5 3 5 = 7, 4 6 = 9
        // ���� �ּһ������� = ������ �������� �ִ밪
        // ���� �ִ� �������� = (������ ���������� �ִ밪)*2-1 + (������ �������� ������ ��)
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
    void SetLine(int layer, int createdLine)//������ �����Ǹ� �װ��������� ���������̸� ����ǵ��Ϲ�ġ
    {
        /*
        1.�������� �������� ������ �������
            ���������� �波�� ���� -> �����ִ� ������ Ȯ�� -> �����ִ� �������� ������ ���������� ���� �߰��� ������ ���� �������μ����ϰ� �ѹ��� ����õ�(n-1 or n+1���游 ����)
        2.�������� �������� ���� ¦���ϰ��
            
        3.�������� �������� ���� Ȧ���ϰ��
         */
    }
}
