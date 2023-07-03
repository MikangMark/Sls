using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegendUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MapNode.ROOMVALUE room_Type;

    Room[] all;
    private void Start()
    {
        all = FindObjectsOfType<Room>();
        Debug.Log(room_Type);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        for(int i = 0; i < all.Length; i++)
        {
            if(room_Type == all[i].node.roomType)
            {
                all[i].gameObject.GetComponent<Image>().color = Color.white;
            }
        }

        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < all.Length; i++)
        {
            if (room_Type == all[i].node.roomType)
            {
                all[i].gameObject.GetComponent<Image>().color = Color.black;
                
            }
        }
    }
}
