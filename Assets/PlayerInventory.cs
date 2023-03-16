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
            case ItemType.Weapon:
                var weapon = ((Weapon)itemSlot.Item);
                if (pm.pc.Weapon == (weapon))
                {
                    pm.pc.Weapon = null;
                    break;
                }
                pm.pc.Weapon = ((Weapon)itemSlot.Item);
                break;
            default:
                break;
        }
    }
}
