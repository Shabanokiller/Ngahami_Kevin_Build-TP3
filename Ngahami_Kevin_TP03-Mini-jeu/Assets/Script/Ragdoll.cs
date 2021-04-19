using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] ragdollRbs;
    Animator animator;
    public bool die;

    // Start is called before the first frame update
    void Awake()
    {
        ragdollRbs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        ToggleRbs(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(die == true)
        {
            die = false;
            Die();
        }
    }

    void Die()
    {
        // Desactiver l'animator
        animator.enabled = false;
        die = true;
        //GetComponent<Zombie>().Dead();
        //Activer le ragdoll
        ToggleRbs(false);
    }

    // Activer/Desactive les rigidbodies du ragdoll
    void ToggleRbs(bool value)
    {
        foreach(Rigidbody rbs in ragdollRbs)
        {
            rbs.isKinematic = value;
        }
    }

}
