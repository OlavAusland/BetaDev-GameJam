using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectiles/Default")]
public class Projectile : ScriptableObject
{
    public float speed;
    public float damage;

    public virtual void OnSpawn(ProjectileManager pm)
    {
        pm.rb.AddForce(Utilities.MouseDirection(pm.transform) * speed, ForceMode2D.Impulse);
    }

    public virtual void OnHit(Transform parent, Transform col)
    {
        Destroy(parent.gameObject);
    }
}
