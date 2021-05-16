using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeArme : MonoBehaviour
{

    public GameObject Premier;
    public GameObject Deuxieme;


    // Start is called before the first frame update
    void Start()
    {
        Premier.SetActive(true);
        Deuxieme.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Premier.SetActive(true);
            Deuxieme.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Premier.SetActive(false);
            Deuxieme.SetActive(true);
        }

    }
}
