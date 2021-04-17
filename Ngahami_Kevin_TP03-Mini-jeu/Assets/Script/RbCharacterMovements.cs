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
    public bool playerGrimpe = false;
    public bool playerGrimpeMur = false;
    public float speedGrimpe = 3f;
    CapsuleCollider playerCollider;

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
        // Grimper a l'echelle
        if (!playerGrimpe)
        {
            moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
        }
        else
        {
            transform.Translate(Vector3.up * speedGrimpe * Time.deltaTime);
        }

        //Grimper au mur
        if (!playerGrimpeMur)
        {
            moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
        }
        else
        {
            transform.Translate(Vector3.up * speedGrimpe * Time.deltaTime);
        }

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

    // Nous permet de decter si notre trigger est en collision avec notre joueur pour produire notre animataion
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            Debug.Log("OK");
            playerGrimpe = true;
            GetComponent<Rigidbody>().useGravity = false;
            animatorEly.SetBool("Escalade", true);
            //Debug.Log("OK");
            //GetComponent<RbCharacterMovements>().enabled = false;
            //Monter = true;
        }

        if (collider.gameObject.tag == "Mur")
        {
            Debug.Log("OK");
            playerGrimpeMur = true;
            GetComponent<Rigidbody>().useGravity = false;
            animatorEly.SetBool("EscaladeMur", true);
            //Debug.Log("OK");
            //GetComponent<RbCharacterMovements>().enabled = false;
            //Monter = true;
        }
    }

    // Nous permet de decter si notre trigger n'est plus en collision avec notre joueur pour annuler notre animataion
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            playerGrimpe = false;
            GetComponent<Rigidbody>().useGravity = true;
            animatorEly.SetBool("Escalade", false);
            transform.Translate(Vector3.forward * 2);
            //Debug.Log("OK");
            //GetComponent<RbCharacterMovements>().enabled = false;
            //Monter = true;
        }

        if (collider.gameObject.tag == "Mur")
        {
            playerGrimpeMur = false;
            GetComponent<Rigidbody>().useGravity = true;
            animatorEly.SetBool("EscaladeMur", false);
            transform.Translate(Vector3.forward * 2);
            //Debug.Log("OK");
            //GetComponent<RbCharacterMovements>().enabled = false;
            //Monter = true;
        }
    }
}
