using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownManager : Singleton<UnknownManager>
{
    [SerializeField]
    GameObject unknownParent;
    [SerializeField]
    List<GameObject> unknownList;
    // Start is called before the first frame update
    void Start()
    {
        Init();

        for(int i = 0; i < unknownParent.transform.childCount; i++)
        {
            unknownList.Add(unknownParent.transform.GetChild(i).gameObject);
            unknownParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        unknownParent.SetActive(false);

    }
    public void ActiveSelectUnknown(int index)
    {
        for(int i = 0; i < unknownList.Count; i++)
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
