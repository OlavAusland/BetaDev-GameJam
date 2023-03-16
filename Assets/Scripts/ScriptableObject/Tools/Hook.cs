using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Hook")]
public class Hook : Weapon
{
    private GameObject prefab => Resources.Load<GameObject>("Weapons/Hook");
    
    public override void Use(Transform origin)
    {
        var hm = Instantiate(prefab, origin.parent.position, Quaternion.identity).GetComponent<HookManager>();

        hm.caller = origin.parent;
        hm.rb.AddForce(Utilities.MouseDirection(origin.parent) * 10, ForceMode2D.Impulse);
    }
}
