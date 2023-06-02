using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    /*
     방이 가지고있을 정보들
        1.이 방의 층위치
        2.방의 정보
        3.이방 다음과 연결될 방의 정보(들)->다음방과 연결될 방정보들을 받아서 선연결
        4.
     */
    public Button mapBtn;
    public MapNode node;
    public EnterRoom enterRoom;

    public List<GameObject> paretRooms;
    public List<GameObject> childRooms;

    public bool activeRoom = false;
    Room()
    {
        node = new MapNode();
        paretRooms = new List<GameObject>();
        childRooms = new List<GameObject>();
    }
    private void Start()
    {
        enterRoom = gameObject.GetComponent<EnterRoom>();
        mapBtn = gameObject.GetComponent<Button>();
        mapBtn.onClick.AddListener(enterRoom.OnClickRoomBtn);
    }
    private void Update()
    {
        switch (node.roomType)
        {
            case MapNode.ROOMVALUE.BOSS:
                GetComponent<Image>().sprite = node.room_Img = Resources.Load<Sprite>("MapUI/hexaghost");
                
                break;
            case MapNode.ROOMVALUE.DEFAULT:
                break;
            case MapNode.ROOMVALUE.ELITE:
                GetComponent<Image>().sprite = node.room_Img = Resources.Load<Sprite>("MapUI/elite");
                break;
            case MapNode.ROOMVALUE.EMPTY:
                break;
            case MapNode.ROOMVALUE.NOMAL:
                GetComponent<Image>().sprite = node.room_Img = Resources.Load<Sprite>("MapUI/monster");
                break;
            case MapNode.ROOMVALUE.REST:
                GetComponent<Image>().sprite = node.room_Img = Resources.Load<Sprite>("MapUI/rest");
                break;
            case MapNode.ROOMVALUE.SHOP:
                GetComponent<Image>().sprite = node.room_Img = Resources.Load<Sprite>("MapUI/shop");
                break;
            case MapNode.ROOMVALUE.TREASURE:
                GetComponent<Image>().sprite = node.room_Img = Resources.Load<Sprite>("MapUI/chest");
                break;
            case MapNode.ROOMVALUE.UNKNOWN:
                GetComponent<Image>().sprite = node.room_Img = Resources.Load<Sprite>("MapUI/event");
                break;
        }
        gameObject.GetComponent<Button>().enabled = activeRoom;
    }

    public void SettingRoom(MapNode data)
    {
        node = data;
    }

}
