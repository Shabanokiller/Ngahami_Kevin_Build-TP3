using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delais = 3f;
    public GameObject effectExplosion;
    public float radius = 20f;
    public float force = 100f;
    public float explose = 90f;
    float countdown;
    bool hasExploded = false;
    public new AudioClip audio;
    private AudioSource audioSource;
    AiSwat Swat;
    public AudioClip shootSound;
    private RaycastHit hit;
    private GameObject[] swat;
    private bool SwatIsdead = false;
    // Start is called before the first frame update
    void Start()
    {
        Swat = FindObjectOfType<AiSwat>();
        countdown = delais;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    // Fonction nous permettant de faire exploser nos bombes 
    void Explode()
    {

        //PlayExplosionSound();

        //On verifie si on est en contact avec un colliders
        //LayerMask rag = LayerMask.GetMask("Ragdoll");
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, 1<<10);

        //On affiche les particules
        Instantiate(effectExplosion, transform.position, transform.rotation);

        PlayExplosionSound();
        //Debug.Break();
        //On applique la force
        foreach (Collider col in colliders)
        {
            // On ajoute de la force
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, 1f, ForceMode.Impulse);
                
            }

            //if(col.GetComponent<Ragdoll>() != null)
            //{
            //    col.GetComponent<AiSwat>().SwatDead();
            //}
            // Les dommages
            //Destruction destruction = col.GetComponent<Destruction>();
            //if (destruction != null)
            //{
            //    destruction.Destroy(); 

            //}
            //Ragdoll ragdoll = col.GetComponentInParent<Ragdoll>();
            if (!SwatIsdead)
            {
                Ragdoll ragdoll = col.GetComponentInParent<Ragdoll>();
                if (ragdoll != null && !SwatIsdead)
                {
                    //swat = GameObject.FindGameObjectsWithTag("Swat");
                    ragdoll.die = true;
                    Debug.Log("L'ennemie a ete touche");
                    PlayDeadSound();
                }
            }


        }

        // On retire la grenade 
        Destroy(gameObject, 2f);
    }

    // Ce qui nous permet d'infliger des degats a nos ennemies 
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Swat"))
    //    {
    //        //GameObject.Find(hit.transform.name).GetComponent<AiSwat>().SwatDead();
    //        //other.gameObject.GetComponent<SwatHealth>().sw
    //        swat.SwatDead();
    //        PlayExplosionSound();
    //    }
    //}

    private void PlayExplosionSound()
    {
        audioSource.PlayOneShot(audio);
    }

    // Ce qui nous permet d'infliger des degats a nos ennemies 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Swat")
        {

            //Debug.Log("player touche");
            Swat.SwatDead();
            //playerStat.Dead();
            //GetComponent<PlayerStat>().Dommage(degats);
            PlayExplosionSound();
            //GameObject.Find("ely_k_atienza").GetComponent<HealthBar>().currentHP -= GameObject.Find("swat").GetComponent<AiSwat>().Degats;
        }
    }

    // fonction pour jouer le son de tir de fusil
    private void PlayDeadSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
}
