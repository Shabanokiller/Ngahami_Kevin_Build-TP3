using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouvements : MonoBehaviour
{

    //Animations du personnage 
    Animation FPSplayer;

    // Vitesse de deplacement 
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;

    public bool isDead;

    public float attackCoolDown;
    private bool isAttacking;
    private float currentCoolDown;

    void Start()
    {
        FPSplayer = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    bool auSol()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f, layerMask:3);
    }

    void Update()
    {

        if (!isDead)
        {
            // Si on avance
            if (Input.GetKey(inputFront))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                FPSplayer.Play("Walk");
            }

            //Si on recule
            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
                FPSplayer.Play("Walk");
            }

            //Rotation a gauche
            if (Input.GetKey(inputLeft))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }

            //Rotation a gauche
            if (Input.GetKey(inputRight))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }

            
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        if(isAttacking)
        {
            currentCoolDown -= Time.deltaTime;
        }
        if (currentCoolDown <= 0)
        {
            currentCoolDown = attackCoolDown;
            isAttacking = false;
        }
    }

    public void Attack()
    {
        isAttacking = true;
        FPSplayer.Play("One Hand Club Combo");
    }
}
