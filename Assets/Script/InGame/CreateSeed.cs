using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class CreateSeed : Singleton<CreateSeed>
{
    public int currentSeed;
    int num = 0;
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        
        currentSeed = Random.Range(0, int.MaxValue);
    }

    public int SeedRandNum(int min,int minPmax)
    {
        Random.InitState(currentSeed);
        return Random.Range(min, minPmax);
    }

    public int RandNum(int min, int minPmax)
    {
        Random.InitState((int)System.DateTime.Now.Ticks + num);
        num++;
        return Random.Range(min, minPmax);
    }
    public int RandNum(List<int> intList)
    {
        Random.InitState((int)System.DateTime.Now.Ticks + num);
        num++;
        return intList[Random.Range(0, intList.Count)];
    }
    public T GetRandomValue<T>(List<T> availableValues)//�ߺ������ʴ� ��������
    {
        // ����Ʈ�� ���� ������ 0�� ��ȯ
        if (availableValues.Count == 0)
        {
            return default(T);
        }

        // ����Ʈ���� ������ �ε����� �����ϰ� �� ���� ��ȯ
        int randomIndex = RandNum(0, availableValues.Count);
        T randomValue = availableValues[randomIndex];

        // ����Ʈ���� �ش� ���� �����ϰ� ��ȯ
        availableValues.RemoveAt(randomIndex);
        return randomValue;
    }

    public bool Roulelet_Per(int persent)//ex)50%70
    {
        if (persent == 0)
        {
            return false;
        }
        if (RandNum(0, 100) <= persent)//100
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
