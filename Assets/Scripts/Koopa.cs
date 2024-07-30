using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    [SerializeField] private Sprite _shellSprite;
    [SerializeField] private float _shellSpeed = 12f;

    private bool _shelled;
    private bool _pushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_shelled && collision.gameObject.CompareTag("Player"))
        {
            if (!_pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.Hit();
            }
        }
        else if (!_shelled && collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        _shelled = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = _shellSprite;
        
    }

    private void PushShell(Vector2 direction)
    {
        _pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.Direction = direction.normalized;
        movement.Speed = _shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (_pushed)
        {
            Destroy(gameObject);
        }
    }
}
