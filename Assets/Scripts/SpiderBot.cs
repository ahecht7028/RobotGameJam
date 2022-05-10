using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBot : Enemy
{
    public GameObject bullet;

    bool canShoot = true;
    int directionMod = 1;
    [SerializeField]
    LayerMask projectileLayer;

    Rigidbody2D rb;
    Animator anim;
    GameObject playerRef;

    enum EnemyState { WANDER, ATTACK }
    EnemyState currentState = EnemyState.WANDER;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerRef = GameObject.Find("Player").gameObject;
    }


    void Update()
    {
        // Line of sight of player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerRef.transform.position - transform.position).normalized, 9999, projectileLayer.value);
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            // Shoot
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
            rb.velocity = Vector2.zero + new Vector2(0, rb.velocity.y);
            currentState = EnemyState.ATTACK;
            if(playerRef.transform.position.x - transform.position.x > 0)
            {
                directionMod = 1;
            }
            else
            {
                directionMod = -1;
            }
        }
        else
        {
            currentState = EnemyState.WANDER;
        }

        // Movement
        if (currentState == EnemyState.WANDER)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(1, -1).normalized, 4);
            if (hit.collider == null)
            {
                directionMod = -1;
            }
            hit = Physics2D.Raycast(transform.position, new Vector2(-1, -1).normalized, 4);
            if (hit.collider == null)
            {
                directionMod = 1;
            }
            hit = Physics2D.Raycast(transform.position, Vector2.left, 4);
            if(hit.collider != null && hit.collider.gameObject.tag == "Ground")
            {
                directionMod = 1;
            }
            hit = Physics2D.Raycast(transform.position, Vector2.right, 4);
            if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
            {
                directionMod = -1;
            }

            rb.velocity = new Vector2(directionMod * 10, rb.velocity.y);
        }

        if(directionMod == 1)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        if(directionMod == 1)
        {
            temp.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(directionMod, 0) * 10;
        temp.transform.position -= new Vector3(0, 1, 0);
        temp.transform.localScale = new Vector3(2, 2, 2);
        anim.Play("SpiderAttack");
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" && currentState == EnemyState.ATTACK)
        {
            // Jump
        }
    }
}
