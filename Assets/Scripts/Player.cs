using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSpriteRenderer _smallRenderer;
    [SerializeField] private PlayerSpriteRenderer _bigRenderer;
    [SerializeField] private DeathAnimation _deathAnimation;
    [SerializeField] private float _resetLevelTime = 3f;

    public bool big => _bigRenderer.enabled;
    public bool small => _smallRenderer.enabled;
    public bool dead => _deathAnimation.enabled;

    public void Hit()
    {
        if (big)
        {
            Shrink();
        }
        else
        {
            Death();
        }
    }

    private void Shrink()
    {

    }

    private void Death()
    {
        _smallRenderer.enabled = false;
        _bigRenderer.enabled = false;
        _deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(_resetLevelTime);
    }
}
