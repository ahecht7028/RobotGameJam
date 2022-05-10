using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    float gravity;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip dashSFX;

    bool isSlidingRight;
    bool isDashing;
    bool canDash;
    bool isAlive;

    Rigidbody2D rb;
    AudioSource audioSource;
    Animator animator;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        movementSpeed = 400f;
        jumpSpeed = 600;
        gravity = 3;
        isSlidingRight = false;
        isDashing = false;
        canDash = true;
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            if (IsWallSliding())
            {
                if (rb.velocity.y > 0)
                {
                    rb.gravityScale = gravity;
                }
                else
                {
                    rb.gravityScale = gravity / 10;
                }

                // Check for Wall Jump
                if (Input.GetButtonDown("Jump"))
                {
                    if (isSlidingRight) { rb.velocity = new Vector2(-jumpSpeed / 80, jumpSpeed / 50); }
                    else { rb.velocity = new Vector2(jumpSpeed / 80, jumpSpeed / 50); }
                    audioSource.PlayOneShot(jumpSFX);
                }
            }
            else
            {
                // Not Wall Sliding
                rb.gravityScale = gravity;
                if (Input.GetButtonDown("Jump") && IsGrounded())
                {
                    rb.AddForce(new Vector2(0, jumpSpeed));
                    audioSource.PlayOneShot(jumpSFX);
                }
            }

            // Dash
            if (Input.GetButtonDown("Fire2") && !isDashing && !IsGrounded() && canDash)
            {
                StartCoroutine(Dash());
                audioSource.PlayOneShot(dashSFX);
            }
        }
    }


    void FixedUpdate()
    {
        if (isAlive)
        {
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * movementSpeed;

            animator.SetFloat("HorizontalSpeed", x);
            animator.SetFloat("VerticalSpeed", rb.velocity.y);

            // If the player is moving on the ground, change velocity directly.
            if (IsGrounded())
            {
                //rb.velocity = new Vector2((rb.velocity.x / 10) + x * 4, rb.velocity.y);
                rb.AddForce(new Vector2(x * 5, 0));
                rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
            }
            // If the player is moving too fast to the right, you can move left.
            else if (rb.velocity.x > Time.deltaTime * movementSpeed && x < 0)
            {
                rb.AddForce(new Vector2(x * 4, 0));
            }
            // If the player is moving too fast to the left, you can move right.
            else if (rb.velocity.x < -Time.deltaTime * movementSpeed && x > 0)
            {
                rb.AddForce(new Vector2(x * 4, 0));
            }
            // If the player is moving at a normal velocity, then the player can move either left or right.
            else if (rb.velocity.x <= Time.deltaTime * movementSpeed && rb.velocity.x >= -Time.deltaTime * movementSpeed)
            {
                rb.AddForce(new Vector2(x * 4, 0));
            }

            if(rb.velocity.x > 0)
            {
                sr.flipX = false;
            }
            if(rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1);
        if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
        {
            // Grounded properties changed here
            isDashing = false;
            canDash = true;
            animator.SetBool("IsGrounded", true);
            return true;
        }
        animator.SetBool("IsGrounded", false);
        return false;
    }

    bool IsWallSliding()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1);
        if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
        {
            // Hit left wall
            isSlidingRight = false;
            return true;
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
        if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
        {
            // Hit right wall
            isSlidingRight = true;
            return true;
        }

        return false;
    }

    IEnumerator Dash()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        canDash = false;
        isDashing = true;
        rb.gravityScale = 0f;
        rb.velocity += direction.normalized * jumpSpeed / 20;
        if(direction.y > 0)
        {
            animator.Play("AmogusDashUp");
        }
        else
        {
            animator.Play("AmogusDashDown");
        }
        yield return new WaitForSeconds(0.2f);
        if (isDashing)
        {
            rb.velocity /= 10;
        }
        rb.gravityScale = gravity;
        isDashing = false;

    }

    public void Die()
    {
        isAlive = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void AnimateShoot()
    {
        animator.Play("AmogusShoot");
    }
}
