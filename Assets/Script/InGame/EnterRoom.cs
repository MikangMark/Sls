using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        if (!inGameUI.battle.thisActive)
        {
            if (gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.ELITE || gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.NOMAL || gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.BOSS)
            {
                Instantiate(inGameUI.clearCircle, gameObject.transform);
                inGameUI.BattleTurnOn();
            }
            if(gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.SHOP)
            {
                Instantiate(inGameUI.clearCircle, gameObject.transform);
                inGameUI.ShopEnter();
            }
            if (gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.REST)
            {
                Instantiate(inGameUI.clearCircle, gameObject.transform);
                inGameUI.RestEnter();
            }
            if (gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.UNKNOWN)
            {
                Instantiate(inGameUI.clearCircle, gameObject.transform);
                inGameUI.UnkownEnter();
            }
            if (gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.TREASURE)
            {
                Instantiate(inGameUI.clearCircle, gameObject.transform);
                inGameUI.TreasureEnter();
            }
        }
    }
}
