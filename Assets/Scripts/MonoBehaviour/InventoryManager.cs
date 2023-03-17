using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject draggablePreview;
    [SerializeField] private Item selectedItem = null;
    [SerializeField] private ItemSlot previousItemSlot = null;
    private bool isDragging = false;

    private Vector2 mousePosition;

    public void Update()
    {
        if (!isDragging){return;}

        mousePosition = Input.mousePosition;
        draggablePreview.transform.position = mousePosition;
        Debug.Log("Dragging: " + selectedItem.name);
        
        

    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null){ return;}
        var itemSlot = eventData.pointerEnter.transform.GetComponent<ItemSlot>();
        if (itemSlot == null || !itemSlot.Item){return;}

        draggablePreview = Instantiate<GameObject>(Resources.Load<GameObject>("UI/Draggable Preview"), 
            mousePosition, Quaternion.identity, transform.parent);
        draggablePreview.GetComponent<Image>().sprite = itemSlot.Item.icon;
        
        
        previousItemSlot = itemSlot;
        selectedItem = itemSlot.Item;
        itemSlot.Item = null;
        
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null)
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            var itemInfo = Instantiate(Resources.Load<GameObject>("Item"),
                new Vector3(worldPoint.x, worldPoint.y, 0), 
                Quaternion.identity).GetComponent<ItemInformation>();
            itemInfo.item = selectedItem;
        }else {
            var itemSlot = eventData.pointerEnter.transform.GetComponent<ItemSlot>();
            if (itemSlot != null)
            {
                if (itemSlot.Item == null)
                {
                    itemSlot.Item = selectedItem;
                }
                else
                {
                    var temp = itemSlot.Item;
                    itemSlot.Item = selectedItem;
                    previousItemSlot.Item = temp;
                }
            }
        }


        Destroy(draggablePreview);
        previousItemSlot = null;
        selectedItem = null;
        isDragging = false;
    }

    public void OnDrawGizmos()
    {
        if(selectedItem == null){return;}
        
        Gizmos.DrawGUITexture(new Rect(mousePosition.x, mousePosition.y, 
            mousePosition.x + selectedItem.icon.texture.width, mousePosition.y + selectedItem.icon.texture.height), selectedItem.icon.texture);
        Gizmos.DrawWireSphere(mousePosition, 1f);
    }
}
