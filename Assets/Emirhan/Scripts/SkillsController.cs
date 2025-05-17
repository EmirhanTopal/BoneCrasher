using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    [SerializeField] private string playerTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            //hurt anim
            //stun
            if (this.name == "punchRef")
            {
                Debug.Log("5");
            }
            else if (this.name == "kickRef")
            {
                Debug.Log("10");
            }
        }
    }
    
}
