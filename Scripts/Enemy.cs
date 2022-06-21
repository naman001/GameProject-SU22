using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public bool mustPatrol;
    public float speed;
    public Rigidbody2D body;

    public SpriteRenderer sr;

    public Transform groundCheckPos;

    public bool mustTurn;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        sr = GetComponent<SpriteRenderer>();
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers())
        {
            Flip();

        }
        body.velocity = new Vector2(speed * Time.fixedDeltaTime, body.velocity.y);
    }

    private void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        mustPatrol = true;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
