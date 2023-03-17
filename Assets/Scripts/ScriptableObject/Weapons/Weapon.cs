using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Default", menuName = "Weapons/Default")]
public class Weapon : Tool
{
    public Projectile projectile;

    public override void Use(Transform caller)
    {
        var pm = Instantiate(Resources.Load<GameObject>("Projectile"), caller.position, Quaternion.identity)
            .GetComponent<ProjectileManager>();
        pm.projectile = projectile;
    }
}
