using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerManager pm;
    public List<ItemSlot> items;
    
    public void Update()
    {
       if(Input.GetKeyDown(KeyCode.Alpha1)) 
           SelectItem(0);
       else if(Input.GetKeyDown(KeyCode.Alpha2)) 
           SelectItem(1);
       else if(Input.GetKeyDown(KeyCode.Alpha3)) 
           SelectItem(2);
       else if(Input.GetKeyDown(KeyCode.Alpha4)) 
           SelectItem(3);
       else if(Input.GetKeyDown(KeyCode.Alpha5)) 
           SelectItem(4);
       
       if(Input.GetKeyDown(KeyCode.E))
           PickupItem();
    }
    
    private void SelectItem(int index)
    {
        var itemSlot = items[index];

        if (itemSlot.Item is null){return;}

        switch (itemSlot.Item.type)
        {
            case ItemType.Consumable:
                (itemSlot.Item as Consumable)?.Use(pm);
                itemSlot.Item = null;
                break;
            case ItemType.Tool:
            case ItemType.Weapon:
                var tool = ((Tool)itemSlot.Item);
                if (pm.pc.Weapon == (tool))
                {
                    pm.pc.Weapon = null;
                    break;
                }
                pm.pc.Weapon = tool;
                break;
            default:
                break;
        }
    }

    private void PickupItem()
    {
        var ray = pm.cam.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(ray, Vector2.zero);

        if (!hit){ return; }

        if (hit.transform.CompareTag("Item"))
        {
            foreach (var slot in items)
            {
                if (slot.Item != null){ continue; }

                slot.Item = hit.transform.GetComponent<ItemInformation>().item;
                Destroy(hit.transform.gameObject);
                return;
            }
        }
    }
}
