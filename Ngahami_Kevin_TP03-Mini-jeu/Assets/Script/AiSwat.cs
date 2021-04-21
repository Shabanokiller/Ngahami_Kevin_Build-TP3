using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiSwat : MonoBehaviour
{
    public Transform target;
    public float distance;
    public float lookAt = 30f;
    public float ChasseRange = 10f;
    public int Degats = 10;
    public float fireAt = 20f;
    public float fireRate = 2f;
    public float attackTime = 1f;
    public GameObject projectil;
    public GameObject eject;
    public AudioClip SoundFire;
    public AudioClip SoundDead;
    public int force;
    private Animator anim;
    private float nextFire;
    private NavMeshAgent agent;
    private bool SwatIsdead = false;
    private Vector3 pointDepart;
    private Vector3 pointDepartA;
    private Vector3 pointDepartB;
    private bool patrouille = false;
    public float speedWalk = 1f;
    public float speedRun = 6f;
    public GameObject muzzleFlash;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attackTime = Time.deltaTime;
        pointDepart = transform.position;
        pointDepartB = transform.Find("pointB").GetComponent<Transform>().transform.position;
        pointDepartA = transform.position;
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector3.Distance(target.position, transform.position);

        // On repositionne nos points
        transform.Find("pointB").GetComponent<Transform>().transform.position = pointDepartB;
        transform.Find("pointA").GetComponent<Transform>().transform.position = pointDepartA;

        // On verifie qu'on est au point de depart
        if(transform.position.x == pointDepartA.x && transform.position.z == pointDepartA.z)
        {
            agent.speed = speedWalk;
            agent.stoppingDistance = 0f;
            patrouille = true;
            agent.SetDestination(pointDepartB);
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
        }

        // On verifie qu'on est au point de d'arriver
        if (transform.position.x == pointDepartB.x && transform.position.z == pointDepartB.z)
        {
            agent.speed = speedWalk;
            agent.stoppingDistance = 0f;
            patrouille = true;
            agent.SetDestination(pointDepartA);
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
        }


        // VErification a distance
        if (Vector3.Distance(target.position, transform.position) < lookAt)
        {
            SwatRun();
            patrouille = false;
        }
        else
        {
            //anim.SetBool("Run", false);
            if(patrouille == false)
            {
                SwatWalk();
            }
        }

        //Verification distance Tir
        if(Vector3.Distance(target.position, transform.position) < fireAt)
        {
            SwatFire();
        }
    }

    void SwatRun()
    {
        //animation courir du swat
        agent.stoppingDistance = 3f;
        agent.speed = speedRun;
        if (agent.velocity.magnitude > 0.1)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }
        agent.updatePosition = true;
        agent.SetDestination(target.position);

        //rotation vers la cible
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 20 * Time.deltaTime);
    }


    void SwatWalk()
    {
        //animation marcher du swat
        agent.stoppingDistance = 0f;
        agent.speed = speedWalk;
        if (agent.velocity.magnitude > 0.1)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }
        agent.updatePosition = true;
        agent.SetDestination(pointDepart);


        //rotation vers la cible
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 20 * Time.deltaTime);
    }

    // Lorsque notre swat s'apprette a tirer 
    void SwatFire()
    {
        Debug.DrawRay(eject.transform.position, transform.TransformDirection(Vector3.forward * 30));

        // On verifie qu'il n'est pas mort et ensuite il effectue son tir
        if (Time.time > nextFire && !SwatIsdead)
        {
            nextFire = Time.time + fireRate;

            RaycastHit hit;

            if (Physics.Raycast(eject.transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    // projectil
                    GetComponent<AudioSource>().PlayOneShot(SoundFire);
                    GameObject bullet = Instantiate(projectil, eject.transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * force);
                    target.SendMessage("ApllicationDesDommages", Degats);
                    Debug.Log("Ennemie a attaque");
                    attackTime = Time.time + fireRate;
                    muzzleFlash.SetActive(true);
                }
            }
        }
    }

    // lorsque le swat meurt 
    public void SwatDead()
    {
        // on le tue
        SwatIsdead = true;
        // On fait en sorte qui n'avance plus et on desactive l'animation
        agent.speed = 0;
        GetComponent<Collider>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(SoundDead);
        anim.SetBool("Dead", true);
    }

    // Le delais d'attente pour notre muzzle flash
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
    }
}
