using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _framerate = 1f / 6f;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _frame;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), _framerate, _framerate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        _frame++;

        if (_frame >= _sprites.Length)
        {
            _frame = 0;
        }

        if (_frame >= 0 && _frame < _sprites.Length)
        {
            _spriteRenderer.sprite = _sprites[_frame];
        }
    }
}

