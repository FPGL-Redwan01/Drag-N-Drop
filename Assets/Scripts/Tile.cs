using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IMoveable
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float moveSpeed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveDirection)
    {
        _rigidbody.velocity = new Vector2(moveDirection.x * moveSpeed, _rigidbody.velocity.y);
    }
}
