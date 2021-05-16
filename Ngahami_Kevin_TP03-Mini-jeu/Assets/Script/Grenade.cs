using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delais = 3f;
    public GameObject effectExplosion;
    public float radius = 5f;
    public float force = 10.0f;
    public float explose = 90f;
    float countdown;
    bool hasExploded = false;
    public new AudioClip audio;
    private AudioSource audioSource;
    AiSwat Swat;
    public AudioClip shootSound;
    private RaycastHit hit;
    private GameObject[] swat;
    // Start is called before the first frame update
    void Start()
    {
        Swat = GameObject.Find("swat").GetComponent<AiSwat>();
        countdown = delais;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //On applique la force
        foreach (Collider col in colliders)
        {
            //Ragdoll ragdoll = hit.collider.GetComponentInParent<Ragdoll>();
            //if (ragdoll != null)
            //{
            //    swat = GameObject.FindGameObjectsWithTag("Swat");
            //    ragdoll.die = true;
            //    Debug.Log("L'ennemie a ete touche");
            //    PlayDeadSound();
            //}

            // On ajoute de la force
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, explose);
                
            }
            // Les dommages
            Destruction destruction = col.GetComponent<Destruction>();
            if (destruction != null)
            {
                destruction.Destroy(); 
                Ragdoll ragdoll = hit.collider.GetComponentInParent<Ragdoll>();
                if (ragdoll != null)
                {
                    swat = GameObject.FindGameObjectsWithTag("Swat");
                    ragdoll.die = true;
                    Debug.Log("L'ennemie a ete touche");
                    PlayDeadSound();
                }
                PlayExplosionSound();
            }
                
        }

        //On affiche les particules
        Instantiate(effectExplosion, transform.position, transform.rotation);
        // On retire la grenade 
        Destroy(gameObject);
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
