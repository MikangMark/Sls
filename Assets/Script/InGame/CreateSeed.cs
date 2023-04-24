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
    /*
    public static T GetRandomUniqueValue<T>(this List<T> list, T excludeValue)
    {
        List<T> tempList = new List<T>(list);
        tempList.Remove(excludeValue);
        tempList = tempList.Distinct().ToList();
        return tempList[Random.Range(0, tempList.Count)];
    }
    */
}
