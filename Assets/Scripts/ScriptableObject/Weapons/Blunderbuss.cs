using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Blunderbuss")]
public class Blunderbuss : Weapon
{
    public float arc;
    public float pellets;
    public float offset;
    
    
    
    public override void Use(Transform caller)
    {
        var dir = Utilities.MouseDirection(caller.transform);

        for (float i = 0, angle = pellets > 1 ? -arc : 0; i < pellets; i++)
        {
            
            var pm = Instantiate(Resources.Load("Projectile"),
                caller.position + (Vector3)dir * offset, Quaternion.identity).GetComponent<ProjectileManager>();

            float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            pm.transform.rotation = Quaternion.AngleAxis(rotation - angle, Vector3.forward);
            pm.rb.velocity = pm.transform.right * 10;
            angle += (arc / (pellets / 2));
        }
    }
}
