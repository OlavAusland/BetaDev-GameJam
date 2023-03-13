using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stackable Item")]
public class StackableItem : Item
{
    public int maxStack;
    public override bool IsStackable { get => true; }
}
