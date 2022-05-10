using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDealDamage
{
    float _speed = 30f;
    float _lifeSpan = 3f;
    int _damage = 1;

    Vector3 _direction;
    float _lifeTime;
    

    public int Damage { get { return _damage; } }

    private void OnEnable()
    {
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetDirection(mouseLocation, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeHit(this);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }


    public void GetDirection(Vector2 target, Vector2 source)
    {
        _direction = (target - source).normalized;
    }

    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        _lifeTime += Time.deltaTime;
        if (_lifeSpan <= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
