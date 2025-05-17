using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillsController : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private Animator _enemyAnimator;
    private CharacterMovement _characterMovement;
    private bool _getControlled = true;
    private AudioManagers _audioManagers;
    private cameraShake _cameraShake;
    

    private void Start()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();
        _audioManagers = FindObjectOfType<AudioManagers>();
        _cameraShake = FindObjectOfType<cameraShake>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            _enemyAnimator.SetTrigger("trg_hurt");
            if (this.name == "punchRef")
            {
                _audioManagers.PlayOneShotClip(_audioManagers._punchClip);
                _characterMovement.TakeDamage(5);
                _cameraShake.ShakeFunc(0.2f, 0.2f);
            }
            else if (this.name == "kickRef")
            {
                _audioManagers.PlayOneShotClip(_audioManagers._kickClip);
                _characterMovement.TakeDamage(10);
                _cameraShake.ShakeFunc(0.5f, 0.5f);
            }
            if (_characterMovement._control)
            {
                _characterMovement.ApplyStun(0.4f);
            }
        }
    }
    
}
