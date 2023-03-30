using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : Singleton<InGame>
{
    public Character.CharInfo charInfo;
    private void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        //charInfo = Character.Instance.ironclead;
        switch (PlayerPrefs.GetInt("CharType")) //��â���������� ĳ��
        {
            case 0:
                charInfo = Character.Instance.ironclead;//���̾�Ŭ����
                break;
            case 1:
                charInfo = Character.Instance.silence;//���Ϸ���
                break;
            case 2:
                charInfo = Character.Instance.defact;//����Ʈ
                break;
            case 3:
                charInfo = Character.Instance.wacher;//����
                break;
        }
    }
}
