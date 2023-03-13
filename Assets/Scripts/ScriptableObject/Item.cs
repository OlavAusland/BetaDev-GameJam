using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable
}

[CreateAssetMenu(fileName = "Default Item")]
public class Item : ScriptableObject
{
    public string name;
    public Sprite icon;
    public ItemType type;

    public virtual bool IsStackable { get => true; }
}
