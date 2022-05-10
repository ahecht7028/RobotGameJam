using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] int _health = 3;

    [HideInInspector]
    public SpriteRenderer sr;

    public int Health { get { return _health; } }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var projectile = collision.collider.GetComponent<Projectile>();

        if (projectile == null)
            return;

        TakeHit(projectile);
    }

    public void TakeHit(IDealDamage attacker)
    {
        _health -= attacker.Damage;
        StartCoroutine(Flash(Color.red));
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator Flash(Color color)
    {
        sr = GetComponent<SpriteRenderer>();
        for(int i = 0; i < 5; i++)
        {
            sr.color = color;
            yield return new WaitForSeconds(0.05f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
