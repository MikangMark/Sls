using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownManager : MonoBehaviour
{
    [SerializeField]
    GameObject unknownParent;
    [SerializeField]
    List<GameObject> unknownList;
    public UnknownDisCard unknownDisCard;
    void Start()
    {
        for(int i = 0; i < unknownList.Count; i++)
        {
            unknownList[i].SetActive(false);
        }
        unknownParent.SetActive(false);

    }
    public void ActiveSelectUnknown(int index)
    {
        unknownParent.SetActive(true);
        for (int i = 0; i < unknownList.Count; i++)
        {
            if (i == index)
            {
                unknownList[i].SetActive(true);
            }
            else
            {
                unknownList[i].SetActive(false);
            }
        }
    }

    public void ExitUnknown()
    {
        InGame.Instance.currentFloor++;
        unknownParent.SetActive(false);
    }
}
