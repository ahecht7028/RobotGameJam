using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrawler : Enemy
{
    public GameObject bullet;

    bool canShoot = true;
    int directionMod = 1;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Line of sight of player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if(hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            // Shoot
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }

        // Movement
        hit = Physics2D.Raycast(transform.position, new Vector2(1, 1).normalized, 2);
        if(hit.collider == null)
        {
            directionMod = -1;
        }
        hit = Physics2D.Raycast(transform.position, new Vector2(-1, 1).normalized, 2);
        if (hit.collider == null)
        {
            directionMod = 1;
        }

        rb.velocity = new Vector2(directionMod * 10, rb.velocity.y);
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject temp = Instantiate(bullet, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
        temp.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10;
        temp.GetComponent<WallCralwerProjectile>()._damage = 3;
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
}
