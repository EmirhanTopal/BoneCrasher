using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillsController : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private GameObject _enemy;
    private Animator _enemyAnimator;
    private SpriteRenderer _enemySpriteRenderer;
    private CharacterMovement _characterMovement;
    private bool _getControlled = true;
    private AudioManagers _audioManagers;
    private cameraShake _cameraShake;
    private int _enemyDir;

    private void Start()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();
        _audioManagers = FindObjectOfType<AudioManagers>();
        _cameraShake = FindObjectOfType<cameraShake>();
        _enemyAnimator = _enemy.GetComponent<Animator>();
        _enemySpriteRenderer = _enemy.GetComponent<SpriteRenderer>();
        if (_enemySpriteRenderer.flipX)
        {
            _enemyDir = 1;
        }
        else
        {
            _enemyDir = -1;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            _enemyAnimator.SetTrigger("trg_hurt");
            if (this.name == "punchRef")
            {
                //kapalı sağ (-1 sola ittiricem) açık sol (sağa ittiricem 1)
                _audioManagers.PlayOneShotClip(_audioManagers._punchClip);
                _characterMovement.TakeDamage(5);
                _enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(_enemyDir * 50f, 3f);
                _cameraShake.ShakeFunc(0.2f, 0.2f);
            }
            else if (this.name == "kickRef")
            {
                _audioManagers.PlayOneShotClip(_audioManagers._kickClip);
                _characterMovement.TakeDamage(10);
                _enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(_enemyDir * 50f, 3f);
                _cameraShake.ShakeFunc(0.3f, 0.3f);
            }
            if (_characterMovement._control)
            {
                _characterMovement.ApplyStun(0.25f);
            }
        }
    }
    
}
