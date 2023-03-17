using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public Projectile projectile;

    private void Start()
    {
        projectile.OnSpawn(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            var pesant = col.GetComponent<Pesant>();
            if (pesant == null) { return;}
            pesant.Health -= (int)projectile.damage;
            projectile.OnHit(transform.transform, col.transform);
        }
    }
}
