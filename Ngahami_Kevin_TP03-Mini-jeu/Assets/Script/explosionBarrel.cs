using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBarrel : MonoBehaviour
{
    //public GameObject Barrel;
    public GameObject explosion;

    private AudioSource source;
    private void Awake()
    {
        //Barrel.SetActive(true);
        explosion.SetActive(true);

        source = this.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    public void Explode()
    {
        //Barrel.SetActive(true);
        explosion.SetActive(true);
        source.Play();
    }
}
