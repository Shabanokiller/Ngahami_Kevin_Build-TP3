using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int grenades = 50;
    public int healthBar = 100;
    public int healthMax = 100;
    private Rigidbody rb;

    private bool isGrounded = true;

    private Animator animatorEly;

    void Start()
    {
        // Assigner le Rigidbody
        rb = GetComponent<Rigidbody>();

        //Assigner l'animator
        animatorEly = GetComponent<Animator>();
    }

    void Dommage(int degats)
    {
        healthBar -= degats;

        if(healthBar <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Debug.Log("Player Dead");
        //animatorEly.SetTrigger("Dead", );
    }
}
