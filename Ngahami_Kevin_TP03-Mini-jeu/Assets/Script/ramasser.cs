﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ramasser : MonoBehaviour
{
    quete objecteu;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        objecteu = FindObjectOfType<quete>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            objecteu.objectAcquire += 1;
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            Debug.Log("Object obtenue");
            Destroy(gameObject, 0.6f);
        }
    }
}
