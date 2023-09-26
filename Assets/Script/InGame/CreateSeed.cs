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
    public List<T> GetRandomValue<T>(List<T> availableValues,int num)//중복되지않는 랜덤설정 랜덤뽑기원하는 리스트/뽑을갯수
    {
        List<T> tempList = new List<T>(availableValues);
        List<T> resultList = new List<T>();
        // 리스트에 값이 없으면 0을 반환
        if (tempList.Count == 0)
        {
            return resultList;
        }
        for(int i = 0; i < num; i++)
        {
            // 리스트에서 랜덤한 인덱스를 선택하고 그 값을 반환
            int randomIndex = RandNum(0, tempList.Count);
            T randomValue = tempList[randomIndex];
            resultList.Add(randomValue);
            // 리스트에서 해당 값을 제거하고 반환
            tempList.RemoveAt(randomIndex);
        }
        
        return resultList;
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
