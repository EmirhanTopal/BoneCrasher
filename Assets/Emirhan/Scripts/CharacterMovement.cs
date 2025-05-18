using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float health = 100;
    public float _horizontalReadValue;
    public bool p1P2Control = false;
    [SerializeField] private int moveSpeed;
    [SerializeField] private GameObject punchGo;
    [SerializeField] private GameObject kickGo;
    [SerializeField] private CharacterMovement _p1p2CharacterMovement;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image criticalImage;
    public Animator animator;
    private Rigidbody2D _rb2d;
    private bool _verticalValue;
    private bool _isGrounded;
    public bool _control = false;
    private AudioManagers _audioManagers;
    public bool isGeberdi = false;
    public bool canMove = true;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        punchGo.SetActive(false);
        kickGo.SetActive(false);
        _audioManagers = FindObjectOfType<AudioManagers>();
    }

    private void Update()
    {
        if(canMove)
            JustMove();
        if (health <= 30 && !_control)
        {
            _control = true;
            CrticialHealth();
        }

        if (health <= 0)
        {
            SonNefes();
        }
    }

    // #TO-DO
    // push - done
    // credit scene 
    // shake camera - done
    // iki karakterin mapte ilerlemesi - done
    
    public void SetCanMove(bool value, float blockDirection = 0f)
    {
        canMove = value;

        // Eğer hareket engellendiyse, o yöne doğru yürümeye çalışıyorsa durdur
        if (!canMove && Mathf.Sign(_horizontalReadValue) == blockDirection)
        {
            _horizontalReadValue = 0f;
            animator.SetBool("is_walk", false);
        }
    }

    
    private void SonNefes()
    {
        //dead anim
        isGeberdi = true;
        FindObjectOfType<StunHandler>().StunBothPlayers(5f);
        //next scene
    }
    
    public void ApplyStun(float stunSeconds)
    {
        StartCoroutine(GetStun(stunSeconds));
    }

    public void CrticialHealth()
    {
        criticalImage.gameObject.SetActive(true);
        FindObjectOfType<StunHandler>().StunBothPlayers(3.25f);
    }
    
    private IEnumerator GetStun(float stunDuration)
    {
        _p1p2CharacterMovement.GetComponent<PlayerInput>().actions.Disable();
        yield return new WaitForSeconds(stunDuration);
        _p1p2CharacterMovement.GetComponent<PlayerInput>().actions.Enable();
        criticalImage.gameObject.SetActive(false);
    }
    
    public void TakeDamage(float damage)
    {
        healthImage.fillAmount -= (damage / 100f);
        health -= damage;
    }
    
    private void JustMove()
    {
        _rb2d.velocity = new Vector2(_horizontalReadValue * moveSpeed, _rb2d.velocity.y);
    }

    private void JustJump()
    {
        if (_isGrounded)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x,  5);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("is_ground", true);
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("is_ground", false);
            _isGrounded = false;
        }
    }

    public void Movement(InputAction.CallbackContext cbx)
    {
        if (cbx.performed)
        {
            if (p1P2Control && (cbx.control.name is "a" or "d"))
            {
                _horizontalReadValue = cbx.ReadValue<Vector2>().x;
                animator.SetBool("is_walk", true);
            }
        }
        else if (cbx.canceled)
        {
            _horizontalReadValue = 0;
            animator.SetBool("is_walk", false);
        }
    }

    public void Movement2(InputAction.CallbackContext cbx2)
    {
        if (cbx2.performed)
        {
            if (!p1P2Control && (cbx2.control.name is "left"))
            {
                _horizontalReadValue = cbx2.ReadValue<Vector2>().x;
                animator.SetBool("is_walk", true);
            }
        }
        else if (cbx2.canceled)
        {
            _horizontalReadValue = 0;
            animator.SetBool("is_walk", false);
        }
    }
    public void SkillsMovement(InputAction.CallbackContext cbx)
    {
        var cbxName = cbx.control.name;
        if (cbx.started)
        {
            if (p1P2Control)
            {
                if (cbxName is "e" && _isGrounded)
                {
                    animator.SetTrigger("trg_punch");
                    punchGo.SetActive(true);
                }
                else if (cbxName is "q" && _isGrounded)
                {
                    animator.SetTrigger("trg_kick");
                    kickGo.SetActive(true);
                }
                else if (cbxName is "space")
                {
                    animator.SetTrigger("trg_jump");
                    _audioManagers.PlayOneShotClip(_audioManagers._jumpClip);
                    JustJump();
                }
            }
            else
            {
                if (cbxName is "buttonWest" && _isGrounded)
                {
                    animator.SetTrigger("trg_punch");
                    punchGo.SetActive(true);
                }
                else if (cbxName is "buttonEast")
                {
                    animator.SetTrigger("trg_kick");
                    kickGo.SetActive(true);
                }
                else if (cbxName is "buttonSouth" && _isGrounded)
                {
                    animator.SetTrigger("trg_jump");
                    _audioManagers.PlayOneShotClip(_audioManagers._jumpClip);
                    JustJump();
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
