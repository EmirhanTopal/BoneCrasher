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
    

    private void Start()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();
        _audioManagers = FindObjectOfType<AudioManagers>();
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
                Debug.Log("5");
            }
            else if (this.name == "kickRef")
            {
                _audioManagers.PlayOneShotClip(_audioManagers._kickClip);
                _characterMovement.TakeDamage(10);
                Debug.Log("10");
            }
            if (_characterMovement._control)
            {
                _characterMovement.ApplyStun(0.4f); // iki stun aynı anda çalışmıyor
            }
            
        }
    }
    
}
