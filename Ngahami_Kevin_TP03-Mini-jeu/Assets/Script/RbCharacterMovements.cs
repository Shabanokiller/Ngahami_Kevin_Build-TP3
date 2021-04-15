using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbCharacterMovements : MonoBehaviour
{
    public float runningspeed = 5f;
    public float walkinggspeed = 1.5f;
    public float jumpHeight = 1f;
    public float Health = 100f;
    // Transform de la position des pieds
    public Transform feetPosition;

    private float speed = 0.1f;
    private float inputVertical;
    private float inputHorizontal;

    private float deadzone = 0.1f;

    private Vector3 moveDirection;

    private Vector3 spawnPosition;

    private Rigidbody rb;

    private bool isGrounded = true;

    private Animator animatorEly;

    // Start is called before the first frame update
    void Start()
    {
        // Assigner le Rigidbody
        rb = GetComponent<Rigidbody>();

        //Assigner l'animator
        animatorEly = GetComponent<Animator>();

        // Assigner le Spawn position
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifier si l'on touche le sol
        isGrounded = Physics.CheckSphere(feetPosition.position, 0.15f, 1, QueryTriggerInteraction.Ignore);

        // *** Vérifier les inputs du joueur ***
        // Vertical (W, S et Joystick avant/arrière)
        inputVertical = Input.GetAxis("Vertical");
        // Horizontal (A, D et Joystick gauche/droite)
        inputHorizontal = Input.GetAxis("Horizontal");
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;  
        
        // Sauter
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // Courir
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = runningspeed;
            animatorEly.SetFloat("Vertical", inputVertical * 2f);
            animatorEly.SetFloat("Horizontal", inputHorizontal * 2f);
        }
        else
        {
            speed = walkinggspeed;
            animatorEly.SetFloat("Vertical", inputVertical);
            animatorEly.SetFloat("Horizontal", inputHorizontal);
        }
        // Grimper au mur 

        //**** Animations de mouvements *****
        //animatorEly.SetFloat("Horizontal", inputHorizontal);
        //animatorEly.SetFloat("Vertical", inputVertical);
        // Respawn (si on est 15m sous le sol
        if (transform.position.y < -15f)
            transform.position = spawnPosition;
    }

    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
