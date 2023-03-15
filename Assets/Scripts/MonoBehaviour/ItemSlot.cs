using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    public Item Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            if (value == null)
            {
                preview.sprite = null;
                preview.color = new Color(preview.color.r, preview.color.g, preview.color.b, 0);
                return;
            }
            
            preview.color = new Color(preview.color.r, preview.color.g, preview.color.b, 1);
            preview.sprite = item.icon;
        }   
    }

    public bool IsOccupied => Item != null;

    public Image preview;

    private void Start(){ Item = item;}
}
