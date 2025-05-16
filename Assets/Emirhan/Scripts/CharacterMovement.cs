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
            //walk anim
        }
        else if (cbx.canceled)
        {
            _horizontalReadValue = 0;
            //idle anim
        }
    }
    public void SkillsMovement(InputAction.CallbackContext cbx)
    {
        var cbxName = cbx.control.name;
        if (cbx.performed)
        {
            if (cbxName is "e" or "buttonWest")
            {
                //punch anim
                Debug.Log(cbx.control.name);
            }
            else if (cbxName is "q" or "buttonEast")
            {
                //kick anim
            }
            else if (cbxName is "space" or "buttonSouth")
            {
                //jump anim
                if ((cbxName == "space" && cbxName == "q") || (cbxName == "buttonSouth" && cbxName == "buttonEast"))
                {
                    //jump kick
                }
            }
        }
    }
}
