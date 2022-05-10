using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCralwerProjectile : MonoBehaviour, IDealDamage
{
    public int _damage = 3;

    public int Damage => _damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerData>().TakeHit(this);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
