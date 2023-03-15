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
        
        
        selectedItem = itemSlot.Item;
        itemSlot.Item = null;
        
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null)
        {
            selectedItem = null;
            isDragging = false;
            return;
        }

        var itemSlot = eventData.pointerEnter.transform.GetComponent<ItemSlot>();
        if (itemSlot == null){return;}
        
        if (itemSlot.Item == null)
        {
            itemSlot.Item = selectedItem;
        }

        Destroy(draggablePreview);
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
