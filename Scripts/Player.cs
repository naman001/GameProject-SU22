using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 3f;
    private bool isGrounded;
    [SerializeField]
    private float movementX;
    [SerializeField]
    private float movementY;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private readonly string walking_side = "walking";
    private readonly string climbing = "climbing";
    private string Ground_tag = "Ground";
    private string Rock_tag = "rock";
    private bool isTouching = false;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This is start() method from player scripts");       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }
  
    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        //transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * moveForce;
        moveDirection = new Vector2(movementX, movementY).normalized;
        myBody.velocity = new Vector2(moveDirection.x * moveForce, moveDirection.y * moveForce);
    }
    void AnimatePlayer()
    {
        try
        {
            if (isGrounded)
            {
                if (movementX > 0 && movementY > 0 || movementX > 0 && movementY < 0 || movementX < 0 && movementY > 0 || movementX < 0 && movementY < 0)
                {
                    if(movementX > movementY)
                    {
                        myBody.velocity = new Vector2(moveDirection.x * moveForce, 0f);
                    }
                    else
                    {
                        myBody.velocity = new Vector2(0f, moveDirection.y * moveForce);
                        isGrounded = false;
                        setClimber(true);
                    }
                }

                if (movementX > 0)
                {
                    setMover(true, true);
                    Debug.Log("main character is walking forward");
                }

                else if (movementX < 0)
                {
                    setMover(true, false);
                    Debug.Log("main character is walking backward");
                }

                else if(movementY > 0)
                {
                    setClimber(true);
                }

                else
                {
                    setMover();
                    Debug.Log("main character is not moving");
                }
            }
            else
            {
                if (movementX > 0 && movementY > 0 || movementX > 0 && movementY < 0 || movementX < 0 && movementY > 0 || movementX < 0 && movementY < 0)
                {
                    if(movementY > movementX)
                    {
                        myBody.velocity = new Vector2(0f, moveDirection.y * moveForce);
                    }
                    else
                    {
                        myBody.velocity = new Vector2(moveDirection.x * moveForce, 0f);
                    }
                }

                if (movementX > 0)
                {
                    setClimber(true);
                    Debug.Log("the main character is climbing forward");
                }
                else if (movementX < 0)
                {
                    setClimber(true);
                    Debug.Log("the main character is climbing backward");
                }
                else if (movementY > 0)
                {
                    setClimber(true);
                    Debug.Log("the main character is climbing upward");
                }
                else if (movementY < 0)
                {
                    setClimber(true);
                    Debug.Log("the main character is climbing downward");
                }
                else
                {
                    setClimber(true);
                    Debug.Log("we're not on the ground");
                }

            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void setClimber(bool climb)
    {
        anim.SetBool(climbing, climb);
        isGrounded = false;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    
    public void setMover(bool walk, bool flip)
    {
        anim.SetBool(walking_side, walk);
        sr.flipX = flip;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void setMover()
    {
        anim.SetBool(walking_side, false);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Ground_tag))
        {
            anim.SetBool(climbing, false);
            isGrounded = true;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log("we landed on ground");
        }

        if (collision.gameObject.CompareTag(Rock_tag))
        {
            isTouching = true;
            Debug.Log("main character has touched the rock!");
            myBody.velocity = new Vector2(0f,0f);
        }
    }
}
