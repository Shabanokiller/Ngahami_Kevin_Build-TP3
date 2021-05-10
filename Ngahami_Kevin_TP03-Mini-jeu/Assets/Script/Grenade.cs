using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delais = 3f;
    public GameObject effectExplosion;
    public float radius = 5f;
    public float force = 500f;
    float countdown;
    bool hasExploded = false;
    public new AudioClip audio;
    private AudioSource audioSource;
    AiSwat swat;
    // Start is called before the first frame update
    void Start()
    {
        swat = GameObject.Find("swat").GetComponent<AiSwat>();
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
        //On affiche les particules
        Instantiate(effectExplosion, transform.position, transform.rotation);
        //PlayExplosionSound();


        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {

            // On ajoute de la force
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
            // Les dommages
            Destruction destruction = col.GetComponent<Destruction>();
            if (destruction != null)
            {
                destruction.Destroy();
                PlayExplosionSound();
            }
                
        }

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
            swat.SwatDead();
            //playerStat.Dead();
            //GetComponent<PlayerStat>().Dommage(degats);
            PlayExplosionSound();
            //GameObject.Find("ely_k_atienza").GetComponent<HealthBar>().currentHP -= GameObject.Find("swat").GetComponent<AiSwat>().Degats;
        }
    }
}
