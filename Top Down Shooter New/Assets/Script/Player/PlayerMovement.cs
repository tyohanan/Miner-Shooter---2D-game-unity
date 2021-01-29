using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animSprite;

    private Vector2 movement;
    private bool isRunning;
    private bool isFacingRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animSprite = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x > 0 && isFacingRight)
        {
            transform.Rotate(0,180,0);
            isFacingRight = false;
        }
        else if (movement.x < 0 && !isFacingRight)
        {
            transform.Rotate(0,180,0);
            isFacingRight = true;
        }

        if (Vector2.SqrMagnitude(movement) > 0)
        {
            isRunning = true;
        }
        else isRunning = false;

        animSprite.SetBool("isRunning", isRunning);
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement*speed*Time.fixedDeltaTime);
    }
}
