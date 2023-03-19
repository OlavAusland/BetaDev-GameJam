using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/ChainCannon")]
public class ChainCannon : Tool
{
    public GameObject prefab;
    public bool canShoot = true;
    public float cooldown;

    public override void Use(Transform caller)
    {
        ChainCannonManager ccm = Instantiate(prefab, caller.transform.position, 
            Quaternion.identity).GetComponent<ChainCannonManager>();
        ccm.chainCannon = this;
        ccm.rb.AddForce(Utilities.MouseDirection(caller.parent) * 10, ForceMode2D.Impulse);
    }

    private IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
