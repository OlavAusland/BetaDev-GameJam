using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Hook")]
public class Hook : Tool
{
    public bool canHook = true;
    public float maxDistance = 5f;
    private GameObject prefab => Resources.Load<GameObject>("Weapons/Hook");

    private void Awake() { canHook = true;}
    
    public override void Use(Transform origin)
    {
        if (!canHook) { return;}

        canHook = false;
        var hm = Instantiate(prefab, origin.parent.position, Quaternion.identity).GetComponent<HookManager>();

        hm.caller = origin.parent;
        hm.hook = this;
        hm.rb.AddForce(Utilities.MouseDirection(origin.parent) * 10, ForceMode2D.Impulse);
    }
}
