using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Default", menuName = "Weapons/Default")]
public class Weapon : Item
{
    [System.Serializable]
    public struct WeaponHand
    {
        public Sprite hand;
        public Vector2 offset;
    }

    public List<WeaponHand> hands;
    
    public Projectile projectile;

    public virtual void Use(Transform origin)
    {
        var pm = Instantiate(Resources.Load<GameObject>("Projectile"), origin.position, Quaternion.identity)
            .GetComponent<ProjectileManager>();
        pm.projectile = projectile;
    }
}
