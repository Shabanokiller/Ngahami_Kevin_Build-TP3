using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RamasserMedocs : MonoBehaviour
{
    public int TotalMedocs = 2;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private PlayerStat playerStat;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = GameObject.Find("ely_k_atienza").GetComponent<PlayerStat>();
    }

    // fonction pour jouer le son de mort 
    //private void PlayGetSound()
    //{
    //    audioSource.PlayOneShot(audioClip);
    //}


    private void OnTriggerEnter(Collider other)
    {

        // Si on entre en contact avec la trousse de soins 
        if (other.gameObject.tag == "Medocs")
        {
            // on ramasse et ensuite ca detruit la trousse 
            Destroy(other.gameObject);
            TotalMedocs -= 1;
            //txtInfos.enabled = true;
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            playerStat.healthBar = 100;
            Debug.Log("Vous avez fait le plein de votre barre de vie");
        }
    }
}
