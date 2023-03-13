using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default", menuName = "Consumable/Default")]
public class Consumable : Item
{
    public int health;
    public int stamina;

    public virtual void Use(PlayerManager pm)
    {
        pm.Health += health;
        pm.Stamina += stamina;
    }
}
