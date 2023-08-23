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
            if (gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.ELITE || gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.NOMAL)
            {
                Instantiate(inGameUI.clearCircle, gameObject.transform);
                inGameUI.BattleTurnOn();
            }
            if(gameObject.GetComponent<Room>().node.roomType == MapNode.ROOMVALUE.SHOP)
            {
                Instantiate(inGameUI.clearCircle, gameObject.transform);
                inGameUI.ShopEnter();
            }
        }
    }
}
