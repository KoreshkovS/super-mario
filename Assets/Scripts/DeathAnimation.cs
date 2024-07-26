using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _deathSprite;
    [SerializeField] private float _elapsed = 0f;
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _jumpVelocity = 10f;
    [SerializeField] private float _gravity = -36f;


    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sortingOrder = 10;
        if (_deathSprite != null)
        {
            _spriteRenderer.sprite = _deathSprite;
        }
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        Vector3 velocity = Vector3.up * _jumpVelocity;

        while (_elapsed < _duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += _gravity * Time.deltaTime;
            _elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
