using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pesant : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;
    [SerializeField] private int health = 100;

    public int Health
    {
        get => health;
        set
        {
            if (value <= 0)
            {
                _animator.SetBool("Dead", true);
                this.enabled = false;
            }
            
            health = Mathf.Max(0, (int)Mathf.Min(value, MaxHealth));
        }
    }
    
    private int damage = 10;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Animator _animator;
    [SerializeField] private Vector2 _meleeOffset;
    [SerializeField] private float _meleeRadius;
    private void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Melee"))
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + new Vector3((_meleeOffset.x * (_sr.flipX ? -1 : 1)), _meleeOffset.y , 0), _meleeRadius, transform.forward);
            
            foreach (var hit in hits)
            {
                var player = hit.transform.GetComponent<PlayerManager>();

                if (player != null)
                {
                    _animator.SetTrigger("Melee");
                    player.Health -= damage;

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3((_meleeOffset.x * (_sr.flipX ? -1 : 1)), _meleeOffset.y , 0), _meleeRadius);
    }
}
