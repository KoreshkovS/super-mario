using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector2 _direction = Vector2.left;

    private Vector2 _velocity;

    public Vector2 Direction { get => _direction; set => _direction = value; }
    public float Speed { get => _speed; set => _speed = value; }

    private void Awake()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        _rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        _velocity.x = Direction.x * Speed;
        _velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);

        if (_rigidbody.Raycast(Direction))
        {
            Direction = -Direction;
        }

        if (_rigidbody.Raycast(Vector2.down))
        {
            _velocity.y = Mathf.Max(_velocity.y, 0f);
        }
    }
}
