using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    [SerializeField]
    InGameUI inGameUI;
    private void Start()
    {
        inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
    }
    public void OnClickRoomBtn()
    {
        if(gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.ELITE|| gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.NOMAL)
        {
            inGameUI.BattleTurnOn();
        }
    }
}
