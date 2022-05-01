using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float jumpForce = 7.0f; //to be edited later
    public float speed = 3.0f; // to be edited later
    public float moveDirection;

    private bool jump;
    private bool grounded = true;
    private bool moving;
    private bool oil = false;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    
    
    
    
   

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); //cache
        _spriteRenderer = GetComponent<SpriteRenderer>(); //cache
        
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        
        _rigidbody2D.velocity = new Vector3(speed * moveDirection, _rigidbody2D.velocity.y);
        if (jump == true) //jumping
        {
            _rigidbody2D.velocity = new Vector3(_rigidbody2D.velocity.x, jumpForce);
            jump = false;
        }
    }
    private void Update()
    {

        if (oil != true) // If ground not oil =>
        {
            if (grounded == true && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) //controls are A,D,W
            {
                if (Input.GetKey(KeyCode.A))
                {
                    moveDirection = -1.0f;
                    
                    _spriteRenderer.flipX = true;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    moveDirection = 1.0f;
                    _spriteRenderer.flipX = false;
                }
            }
            else if (grounded == true)
            {
                moveDirection = 0.0f;
            }

            if (grounded == true && Input.GetKey(KeyCode.W))
            {
                jump = true;
                grounded = false;
            }   
        }
        else // if ground oil
        {
            moveDirection = 1.0f;
        }
       
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor")) //background tilemap is tagged 'floor'
        {
            grounded = true;
            oil = false;
        }else if(collision.gameObject.CompareTag("oil")) // Check Ground "oil" or Not??
        {
            oil = true;
        }
    }
}
