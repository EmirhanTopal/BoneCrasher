using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private float _horizontalReadValue;
    [SerializeField] private int moveSpeed;
    private Animator Animator;
    private Rigidbody2D _rb2d;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        JustMove();
    }

    private void JustMove()
    {
        rb2d.velocity = new Vector2(_horizontalReadValue * moveSpeed, 0f);
    }
    
    public void Movement(InputAction.CallbackContext cbx)
    {
        if (cbx.performed)
        {
            _horizontalReadValue = cbx.ReadValue<Vector2>().x;
            Animator.SetBool("is_walk", true);
        }
        else if (cbx.canceled)
        {
            _horizontalReadValue = 0;
            Animator.SetBool("is_walk", false);
        }
    }
    public void SkillsMovement(InputAction.CallbackContext cbx)
    {
        var cbxName = cbx.control.name;
        if (cbx.performed)
        {
            if (cbxName is "e" or "buttonWest")
            {
                Animator.SetTrigger("trg_punch");
                Debug.Log(cbx.control.name);
            }
            else if (cbxName is "q" or "buttonEast")
            {
                Animator.SetTrigger("trg_kick");
            }
            else if (cbxName is "space" or "buttonSouth")
            {
                Animator.SetTrigger("trg_jump");
                Animator.SetBool("is_ground", false);
                _rb2d.velocity = new Vector2(0,);
                // if ((cbxName == "space" && cbxName == "q") || (cbxName == "buttonSouth" && cbxName == "buttonEast"))
                // {
                //     Animator.SetTrigger("trg_jumpkick");
                // }
            }
        }
    }
}
