using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CreateSeed : Singleton<CreateSeed>
{
    const string SAVE_FILE_PATH = "/randomSeed.dat";
    public int randomSeed;
    void Start()
    {
        CreateRandomSeed();
    }
    void CreateRandomSeed()
    {
        // 저장된 랜덤 시드 값이 있는지 확인합니다.
        if (File.Exists(Application.persistentDataPath + SAVE_FILE_PATH))
        {
            // 저장된 랜덤 시드 값을 파일에서 불러옵니다.
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + SAVE_FILE_PATH);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            randomSeed = (int)formatter.Deserialize(stream);
        }
        else
        {
            // 저장된 랜덤 시드 값이 없으면, 새로운 랜덤 시드 값을 생성합니다.
            randomSeed = (int)System.DateTime.Now.Ticks;

            // 새로운 랜덤 시드 값을 파일로 저장합니다.
            SaveRandomSeed(randomSeed);
        }

        // 랜덤 시드를 설정합니다.
        Random.InitState(randomSeed);
    }

    //질문
    void SaveRandomSeed(int seed)
    {
        // 시드 값을 바이너리로 직렬화합니다.
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, seed);

        // 바이너리 데이터를 파일로 저장합니다.
        string savePath = Application.persistentDataPath + SAVE_FILE_PATH;
        File.WriteAllBytes(savePath, stream.ToArray());
    }
}
