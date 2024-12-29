using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float moveSpeed;
    public float sprintSpeed;
    float speedX, speedY;
    private Animator animator;
    bool isSprinting = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        //POHYB
        rb = GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedX = Input.GetAxis("Horizontal") * sprintSpeed;
            speedY = Input.GetAxis("Vertical") * sprintSpeed;
            isSprinting = true;
            animator.SetBool("Sprinting", true);
        } else
        {
            speedX = Input.GetAxis("Horizontal") * moveSpeed;
            speedY = Input.GetAxis("Vertical") * moveSpeed;
            isSprinting = false;
            animator.SetBool("Sprinting", false);
        }
        rb.velocity = new Vector2(speedX, speedY);

        //VIZUAL - otaceni, anim
        if (Input.GetAxis("Horizontal") != 0 | Input.GetAxis("Vertical") != 0)
        {
            if (isSprinting)
            {
                animator.SetBool("Walking", false);
            } else
            {
                animator.SetBool("Walking", true);
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                sr.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                sr.flipX = false;
            }
        } else
        {
            animator.SetBool("Walking", false);
        }

    }
}
