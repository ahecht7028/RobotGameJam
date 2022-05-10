using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDealDamage
{
    [SerializeField] float _flySpeed = 5;
    [SerializeField] int _damage;

    Vector2 _direction;
    Rigidbody2D _rb;

    public int Damage { get { return _damage; } }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.velocity = _direction.normalized * _flySpeed;
    }

    public void AssignDirection(Vector2 targetDirection)
    {
        _direction = targetDirection;
    }
}
