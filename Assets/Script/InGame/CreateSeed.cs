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
        // ����� ���� �õ� ���� �ִ��� Ȯ���մϴ�.
        if (File.Exists(Application.persistentDataPath + SAVE_FILE_PATH))
        {
            // ����� ���� �õ� ���� ���Ͽ��� �ҷ��ɴϴ�.
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + SAVE_FILE_PATH);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            randomSeed = (int)formatter.Deserialize(stream);
        }
        else
        {
            // ����� ���� �õ� ���� ������, ���ο� ���� �õ� ���� �����մϴ�.
            randomSeed = (int)System.DateTime.Now.Ticks;

            // ���ο� ���� �õ� ���� ���Ϸ� �����մϴ�.
            SaveRandomSeed(randomSeed);
        }

        // ���� �õ带 �����մϴ�.
        Random.InitState(randomSeed);
    }

    //����
    void SaveRandomSeed(int seed)
    {
        // �õ� ���� ���̳ʸ��� ����ȭ�մϴ�.
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, seed);

        // ���̳ʸ� �����͸� ���Ϸ� �����մϴ�.
        string savePath = Application.persistentDataPath + SAVE_FILE_PATH;
        File.WriteAllBytes(savePath, stream.ToArray());
    }
}
