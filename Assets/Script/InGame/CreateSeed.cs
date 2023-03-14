using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CreateSeed : Singleton<CreateSeed>
{
    public int currentSeed;
    int num = 0;
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
   
}
