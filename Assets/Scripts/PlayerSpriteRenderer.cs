using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerMovement _movement;

    [Space]
    [SerializeField] private Sprite _idle;
    [SerializeField] private Sprite _jump;
    [SerializeField] private Sprite _slide;
    [SerializeField] private AnimatedSprite _run;

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
        _run.enabled = false;
    }

    private void LateUpdate()
    {
        _run.enabled = _movement.running;

        if (_movement.jumping)
        {
            _spriteRenderer.sprite = _jump;
        }
        else if (_movement.sliding)
        {
            _spriteRenderer.sprite = _slide;
        }
        else if(!_movement.running)
        {
            _spriteRenderer.sprite = _idle;
        }
    }
}
