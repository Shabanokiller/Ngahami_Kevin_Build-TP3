using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RamasserMedocs : MonoBehaviour
{
    public int TotalMedocs = 2;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        //txtInfos = GameObject.Find("Txtinfos").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si on entre en contact avec la trousse de soins 
        if(other.gameObject.tag == "Medocs")
        {
            // on ramasse et ensuite ca detruit la trousse 
            Destroy(other.gameObject);
            TotalMedocs -= 1;
            //txtInfos.enabled = true;
            GetComponent<AudioSource>().PlayOneShot(audioClip);
        }
    }
}
