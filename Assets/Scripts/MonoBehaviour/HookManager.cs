using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    public Transform caller;
    public Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private bool isHooked = false;
    
    private Vector3 _startHook;
    private Vector3 _hitHook;
    private float fraction = 0;
    private float speed = 1.5f;

    public void Start()
    {
        if(caller != null)
            lr.SetPosition(0, caller.position);
    }

    public void Update()
    {
        transform.up = -Utilities.Direction(caller.position, transform.position);
        lr.SetPosition(1, transform.position);
        if(caller != null)
            lr.SetPosition(0, caller.position);

        if (isHooked)
        {
            if (fraction < 1)
            {
                fraction += Time.deltaTime * speed;
                caller.position = Vector3.Lerp(_startHook, _hitHook, fraction);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            isHooked = true;
            rb.velocity = Vector3.zero;
            _startHook = caller.position;
            _hitHook = col.transform.position;
        }
    }
}
