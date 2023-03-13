using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] public PlayerManager pm;
    public KeyCode meleeAttack = KeyCode.F;

    [Space(20)] [Header("Melee")] 
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _meleeRadius = 0.25f;
    [SerializeField] private Vector2 _meleeOffset = new Vector2(0.3f, 0.4f);
    
    [Space(20)] [Header("Range")] 
    [SerializeField] private Weapon weapon;
    public Weapon Weapon
    {
        get { return weapon;}
        set
        {
            weaponTransform.eulerAngles = Vector3.zero;
            foreach(Transform child in weaponTransform)
                Destroy(child.gameObject);
            weapon = value;
            if (value is null)
            {
                weaponSprite.sprite = null;
                pm._animator.SetBool("WeaponEquipped", false);
                
                return;
            }

            foreach (var weaponHand in weapon.hands)
            {
                var hand = Instantiate(new GameObject(), 
                    weaponTransform.position + (Vector3)weaponHand.offset, Quaternion.identity, weaponTransform);
                hand.AddComponent<SpriteRenderer>();
                hand.GetComponent<SpriteRenderer>().sprite = weaponHand.hand;
                hand.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
                
                
            pm._animator.SetBool("WeaponEquipped", true);
            weaponSprite.sprite = weapon.icon;
        }
    }

    public SpriteRenderer weaponSprite;
    public Transform weaponTransform;
    
    
    public void Update()
    {
        if(Input.GetKeyDown(meleeAttack))
            MeleeAttack();
        if (Weapon is not null)
        {
            Rotate();
            RangedAttack();
        }
        
        
        if (Input.GetKeyUp(KeyCode.O))
        {
            pm.Health += 10;
        }else if (Input.GetKeyUp(KeyCode.P))
        {
            pm.Health -= 10;
        }
    }

    private void MeleeAttack()
    {
        pm._animator.SetTrigger("Melee");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            transform.position + new Vector3((float)(_meleeOffset.x * (pm._sr.flipX ? -1 : 1)), _meleeOffset.y, 0), _meleeRadius,
            transform.forward, _layerMask);

        foreach (var hit in hits)
        {
            var enemy = hit.transform.GetComponent<Pesant>();

                Debug.Log(hit);
            if (enemy != null)
            {
                enemy.Health -= 10;
            }
        }
    }

    private void RangedAttack()
    {

        Weapon.Use();
    }

    private Vector2 Direction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        return direction;
    }
    
    void FlipWeapon()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        weaponTransform.localScale = new Vector3(1, mouse.x < transform.position.x ? -1 : 1, 1);
    }
    
    private void Rotate()
    {
        FlipWeapon();
        Vector2 dir = Direction();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //weaponTransform.rotation = Quaternion.AngleAxis(angle + _weapon.rotationOffset * (int)(sr.flipY ? -1 : 1), Vector3.forward);
        weaponTransform.rotation = Quaternion.AngleAxis(angle * (int)(pm._sr.flipY ? -1 : 1), Vector3.forward);
    }
    
    

    private void OnDrawGizmos()
    {
        if (pm._animator.GetCurrentAnimatorStateInfo(0).IsName("Melee"))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3((float)(_meleeOffset.x * (pm._sr.flipX ? -1 : 1)), _meleeOffset.y , 0), _meleeRadius);
        }
    }
}
