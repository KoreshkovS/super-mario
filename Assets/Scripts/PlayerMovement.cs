using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _maxJumpHeight = 5f;
    [SerializeField] private float _maxJumpTime = 1f;

    private Vector2 _velocity;
    private float _inputAxis;

    public float jumpForce => (2f * _maxJumpHeight) / (_maxJumpTime / 2f);
    public float gravity => (-2f * _maxJumpHeight) / Mathf.Pow(_maxJumpTime / 2f, 2f);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(_velocity.x) > 0.25f || Mathf.Abs(_inputAxis) > 0.25f;
    public bool sliding => (_inputAxis > 0 && _velocity.x < 0f) || (_inputAxis < 0 && _velocity.x > 0f);

    private void Update()
    {
        HorizintalMovement();

        grounded = _rigidbody.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;
        position += _velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = _camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        _rigidbody.MovePosition(position);
    }

    private void HorizintalMovement()
    {
        _inputAxis = Input.GetAxis("Horizontal");
        _velocity.x = Mathf.MoveTowards(_velocity.x, _inputAxis * _moveSpeed, _moveSpeed * Time.deltaTime);

        if (_rigidbody.Raycast(Vector2.right *_velocity.x))
        {
            _velocity.x = 0f;
        }

        if (_velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (_velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        _velocity.y = Mathf.Max(_velocity.y, 0f);
        jumping = _velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = _velocity.y < 0f || !Input.GetButton("Jump");
        float multipier = falling ? 2f : 1f;

        _velocity.y += gravity * multipier * Time.deltaTime;
        _velocity.y = Mathf.Max(_velocity.y, gravity / 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                _velocity.y = 0f;
            }
        }
    }
}
