using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionZone : MonoBehaviour
{
    //public AudioClip audio;
    private AudioSource audioSource;
    public int temps = 10;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<AudioSource>().PlayOneShot(audio);
        
        wait();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "swat")
        {
            other.gameObject.GetComponent<SwatHealth>().swatHealth -= 50;
            //PlayExplosionSound();
        }
    }

    //private void PlayExplosionSound()
    //{
    //    audioSource.PlayOneShot(audio);
    //}

    // Le delais d'attente 
    IEnumerator wait()
    {
        yield return new WaitForSeconds(temps);
        //PlayExplosionSound();
    }
}
