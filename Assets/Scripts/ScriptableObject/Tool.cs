using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    [System.Serializable]
    public struct WeaponHand
    {
        public Sprite hand;
        public Vector2 offset;
    }

    public List<WeaponHand> hands;

    public virtual void Use(Transform caller){}
}
