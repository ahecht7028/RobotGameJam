using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBot : Enemy, IDealDamage
{
    int _damage = 2;

    public int Damage => _damage;

    float patrolSpeed = 3;
    float runSpeed = 10;
    float projectileSpeed = 10;
    float spread = 40;
    float rateOfFire = 1;
    bool moveRight = false;
    Rigidbody2D rb;
    SpriteRenderer srC;
    Vector3 startingPos;
    Vector3 wanderPos;
    GameObject playerRefB;
    public GameObject projectile;
    float timer = 0;
    bool canSeePlayer;
    float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        srC = GetComponent<SpriteRenderer>();
        playerRefB = GameObject.Find("Player").gameObject;
        startingPos = transform.position;
        wanderPos = transform.position;
    }


    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerRefB.transform.position - transform.position).normalized);
        canSeePlayer = hit.collider != null && hit.collider.gameObject.tag == "Player";

        if (canSeePlayer)
        {
            timer -= Time.deltaTime;
            speed = runSpeed;
            if (timer <= 0)
            {
                timer = rateOfFire;
                // Shoot Player

                //Create a new bullet
                GameObject temp = Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
                //Get direction of player
                Vector2 playerDirection = (playerRefB.transform.position - transform.position).normalized;
                //Rotate the bullet in the right direction
                temp.transform.up = -playerDirection;
                //Shoot the bullet in the way we're facing
                temp.GetComponent<Rigidbody2D>().velocity = playerDirection * projectileSpeed;
                //Spread the bullet(s) out -- a faster projectile speed makes this more difficult to tell
                temp.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread)));
                temp.GetComponent<WallCralwerProjectile>()._damage = 2;
                temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
        else
        {
            speed = patrolSpeed;
        }
        // If at destination, pick a new one
        //Debug.Log(Vector3.Distance(transform.position, wanderPos));
        if (Vector3.Distance(transform.position, wanderPos) < 0.1)
        {
            wanderPos = new Vector3(startingPos.x + Random.Range(-5f, 5f), startingPos.y + Random.Range(-5f, 5f), 0);
        }

        //rb.AddForce((wanderPos - transform.position).normalized * speed / 10);
        rb.velocity = new Vector2(wanderPos.x - transform.position.x, wanderPos.y - transform.position.y).normalized * speed;

        moveRight = rb.velocity.x > 0;

        UpdateSprite();
    }

    void UpdateSprite()
    {
        srC.flipX = !moveRight;
    }
}
