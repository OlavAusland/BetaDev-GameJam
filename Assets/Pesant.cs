using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pesant : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private int health = 100;

    public int Health
    {
        get => health;
        set
        {
            if (value <= 0)
            {
                _animator.SetBool("Dead", true);
                Destroy(this);
            }
            
            if ((value - health) < 0)
            {
                Instantiate(Resources.Load<GameObject>("Effects/Blood"), transform.position, Quaternion.identity);
            }
            
            healthBar.fillAmount = (value / MaxHealth);
            
            health = Mathf.Max(0, (int)Mathf.Min(value, MaxHealth));
        }
    }
    
    private int damage = 10;
    public Image healthBar;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Animator _animator;
    [SerializeField] private Vector2 _meleeOffset;
    [SerializeField] private float _meleeRadius;

    private void Start()
    {
        if (gm == null) { gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); }
        healthBar = Instantiate(
            Resources.Load<GameObject>("UI/Health"), gm.worldCanvas)
            .transform.GetChild(0).GetComponent<Image>();
    }
    
    private void Update()
    {
        healthBar.transform.parent.position = transform.position + new Vector3(0, 0.25f, 0);
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
    
    protected void Move()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3((_meleeOffset.x * (_sr.flipX ? -1 : 1)), _meleeOffset.y , 0), _meleeRadius);
    }
    
    private void OnDestroy()
    {
        Destroy(GetComponent<CircleCollider2D>());
        if (healthBar == null) { return;}
        Destroy(healthBar.transform.parent.gameObject);
    }
}
