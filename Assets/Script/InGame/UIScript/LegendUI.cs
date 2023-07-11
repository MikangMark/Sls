using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegendUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MapNode.ROOMVALUE room_Type;
    public MapCreate map;
    public void OnPointerEnter(PointerEventData eventData)
    {
        for(int i = 0; i < map.room_List.Count; i++)
        {
            if(room_Type == map.room_List[i].GetComponent<Room>().node.roomType)
            {
                map.room_List[i].GetComponent<Image>().color = Color.white;
            }
        }

        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < map.room_List.Count; i++)
        {
            if (room_Type == map.room_List[i].GetComponent<Room>().node.roomType)
            {
                map.room_List[i].GetComponent<Image>().color = Color.black;
            }
        }
    }
}
