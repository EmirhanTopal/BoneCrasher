using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillsController : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private Animator _animator;
    private CharacterMovement _characterMovement;
    

    private void Start()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();
    }

    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            _animator.SetTrigger("trg_hurt");
            _characterMovement.ApplyStun();
            if (this.name == "punchRef")
            {
                _characterMovement.TakeDamage(5);
                Debug.Log("5");
            }
            else if (this.name == "kickRef")
            {
                _characterMovement.TakeDamage(10);
                Debug.Log("10");
            }
        }
    }
    
}
