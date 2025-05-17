using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public int health = 100;
    private float _horizontalReadValue;
    public bool _p1P2Control = false;
    [SerializeField] private int moveSpeed;
    [SerializeField] private GameObject punchGo;
    [SerializeField] private GameObject kickGo;

    [SerializeField]  private Image _image;
    private Animator Animator;
    private Rigidbody2D _rb2d;
    private bool verticalValue;
    private bool isGrounded;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        punchGo.SetActive(false);
        kickGo.SetActive(false);
    }

    private void Update()
    {
        JustMove();
    }

    private void TakeDamage(int damage)
    {
        _image.fillAmount -= (damage / 100);
        health -= damage;
    }
    
    private void JustMove()
    {
        _rb2d.velocity = new Vector2(_horizontalReadValue * moveSpeed, _rb2d.velocity.y);
    }

    private void JustJump()
    {
        if (isGrounded)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x,  5);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("sa");
            Animator.SetBool("is_ground", true);
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Animator.SetBool("is_ground", false);
            isGrounded = false;
        }
    }

    public void Movement(InputAction.CallbackContext cbx)
    {
        if (cbx.performed)
        {
            if (_p1P2Control && (cbx.control.name is "a" or "d"))
            {
                Debug.Log(cbx.control.name);
                _horizontalReadValue = cbx.ReadValue<Vector2>().x;
                Animator.SetBool("is_walk", true);
                
            }
        }
        else if (cbx.canceled)
        {
            _horizontalReadValue = 0;
            Animator.SetBool("is_walk", false);
        }
    }

    public void Movement2(InputAction.CallbackContext cbx2)
    {
        if (cbx2.performed)
        {
            if (!_p1P2Control && (cbx2.control.name is "left"))
            {
                Debug.Log(cbx2.control.name);
                _horizontalReadValue = cbx2.ReadValue<Vector2>().x;
                Animator.SetBool("is_walk", true);
            }
        }
        else if (cbx2.canceled)
        {
            _horizontalReadValue = 0;
            Animator.SetBool("is_walk", false);
        }
    }
    public void SkillsMovement(InputAction.CallbackContext cbx)
    {
        var cbxName = cbx.control.name;
        if (cbx.started)
        {
            if (_p1P2Control)
            {
                if (cbxName is "e")
                {
                    Animator.SetTrigger("trg_punch");
                    punchGo.SetActive(true);
                    Debug.Log(cbx.control.name);
                }
                else if (cbxName is "q")
                {
                    Animator.SetTrigger("trg_kick");
                    kickGo.SetActive(true);
                }
                else if (cbxName is "space")
                {
                    Animator.SetTrigger("trg_jump");
                    JustJump();
                    // if ((cbxName == "space" && cbxName == "q") || (cbxName == "buttonSouth" && cbxName == "buttonEast"))
                    // {
                    //     Animator.SetTrigger("trg_jumpkick");
                    // }
                }
            }
            else
            {
                if (cbxName is "buttonWest")
                {
                    Animator.SetTrigger("trg_punch");
                    punchGo.SetActive(true);
                    Debug.Log(cbx.control.name);
                }
                else if (cbxName is "buttonEast")
                {
                    Animator.SetTrigger("trg_kick");
                    kickGo.SetActive(true);
                }
                else if (cbxName is "buttonSouth")
                {
                    Animator.SetTrigger("trg_jump");
                    JustJump();
                    // if ((cbxName == "space" && cbxName == "q") || (cbxName == "buttonSouth" && cbxName == "buttonEast"))
                    // {
                    //     Animator.SetTrigger("trg_jumpkick");
                    // }
                }
            }
            
        }
        else if (cbx.canceled)
        {
            punchGo.SetActive(false);
            kickGo.SetActive(false);
        }
    }
}
