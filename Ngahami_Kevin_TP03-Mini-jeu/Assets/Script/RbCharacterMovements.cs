using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Barre d'energie
    public Image Stamina;
    public float StaminaBar = 100f;
    public float StaminaMax = 100f;
    public float speedBarreEnergieVide = 30f;
    public float speedBarreEnergieRempli = 0.3f;
    private float speedBarreEnergie = 1f;
    private float deadzone = 0.1f;
    WeaponManager weaponManager;

    private Vector3 moveDirection;

    //private Vector3 spawnPosition;

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

        weaponManager = FindObjectOfType<WeaponManager>();

        StaminaBar = StaminaMax;
        //UI ui = GetComponent<UI>();
        //ui.SetController(GetComponent<RbCharacterMovements>());
        // Assigner le Spawn position
        //spawnPosition = transform.position;
    }

    // Update is called once per frame
    public void Update()
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
            Stamina.fillAmount -= 0.05f * Time.deltaTime;
            if (Stamina.fillAmount >= 0)
            {
                animatorEly.SetFloat("Vertical", inputVertical * 2f);
                animatorEly.SetFloat("Horizontal", inputHorizontal * 2f);
            }

            if (Stamina.fillAmount <= 0)
            {
                speed = walkinggspeed;
                animatorEly.SetFloat("Vertical", inputVertical);
                animatorEly.SetFloat("Horizontal", inputHorizontal);
            }
            //speedBarreEnergie -= speedBarreEnergieVide * Time.deltaTime;
            
        }
        else
        {
            speed = walkinggspeed;
            Stamina.fillAmount += speedBarreEnergieRempli * Time.deltaTime;
            //speedBarreEnergie += speedBarreEnergieRempli * Time.deltaTime;
            animatorEly.SetFloat("Vertical", inputVertical);
            animatorEly.SetFloat("Horizontal", inputHorizontal);
        }
        // Nous permet de delimiter notre barre d'energie
        speedBarreEnergie = Mathf.Clamp(speedBarreEnergie, 0f, 1f);

        // Cacher
        if (Input.GetKeyDown(KeyCode.J))
        {
            animatorEly.SetBool("Cacher", true);
            //rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        else
        {
            moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
            //animatorEly.SetBool("Cacher", false);
            // *** Vérifier les inputs du joueur ***
            // Vertical (W, S et Joystick avant/arrière)
            //inputVertical = Input.GetAxis("Vertical");
            //// Horizontal (A, D et Joystick gauche/droite)
            //inputHorizontal = Input.GetAxis("Horizontal");
            //moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
        }

        //if (weaponManager.Fire() == true)
        //{

        //}
        // Grimper a l'echelle
        //if (!playerGrimpe)
        //{
        //    moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
        //}
        //else
        //{
        //    transform.Translate(Vector3.up * speedGrimpe * Time.deltaTime);
        //}

        //Grimper au mur
        if (!playerGrimpeMur && Input.GetKey(KeyCode.K))
        {
            animatorEly.SetBool("EscaladeMur", true);
            //transform.Translate(Vector3.up * speedGrimpe * Time.deltaTime);
        }
        else
        {
            animatorEly.SetBool("EscaladeMur", false);
            //moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
        }

        //**** Animations de mouvements *****
        //animatorEly.SetFloat("Horizontal", inputHorizontal);
        //animatorEly.SetFloat("Vertical", inputVertical);
        // Respawn (si on est 15m sous le sol
        //if (transform.position.y < -15f)
        //    transform.position = spawnPosition;
    }

    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    // Nous permet de detecter si notre trigger est en collision avec notre joueur pour produire notre animataion
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Mur")
        {
            //Debug.Log("OK");
            playerGrimpeMur = true;
            GetComponent<Rigidbody>().useGravity = false;
            animatorEly.SetBool("EscaladeMur", true);
        }
    }

    // Nous permet de decter si notre trigger n'est plus en collision avec notre joueur pour annuler notre animataion
    private void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Mur")
        {
            playerGrimpeMur = false;
            GetComponent<Rigidbody>().useGravity = true;
            animatorEly.SetBool("EscaladeMur", false);
            transform.Translate(Vector3.forward * 2);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Mur")
        {
            //Debug.Log("OK");
            playerGrimpeMur = true;
            GetComponent<Rigidbody>().useGravity = false;
            animatorEly.SetBool("EscaladeMur", true);
        }
    }

    public float GetspeedBarreEnergie()
    {
        return speedBarreEnergie;
    }


    public void Feu()
    {
        animatorEly.SetBool("Shoot", true);
        transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void AnnulerFeu()
    {
        animatorEly.SetBool("Shoot", false);
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;
    }
}
