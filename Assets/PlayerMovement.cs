using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public PlayerManager pm;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _velocity;
    public float speed;
    public float runMultiplier = 1.2f; // 20percent
    
    public void FixedUpdate()
    {
        if(!pm._animator.GetCurrentAnimatorStateInfo(0).IsName("Melee"))
            Movement();
    }

    private void Movement()
    {
        var running = Input.GetKey(KeyCode.LeftShift);

        _velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        _rb.MovePosition(transform.position + (Vector3)_velocity * (running ? runMultiplier : 1));

        if (running)
        {
            pm.Stamina -= (int)(Time.deltaTime * 100);
        }
        else {pm.Stamina += (int)(Time.deltaTime * 75);}
        
        pm._animator.SetBool("Walk", _velocity.magnitude > 0);

        pm._sr.flipX = pm._mousePosition.x < transform.position.x;
    }
}
