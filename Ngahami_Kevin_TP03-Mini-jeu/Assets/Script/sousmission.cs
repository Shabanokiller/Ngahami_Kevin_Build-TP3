using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sousmission : MonoBehaviour
{
    public int nbEnnemies = 0;
    private GameObject[] Swat;
    private GameObject[] Boss;

    // Start is called before the first frame update
    void Start()
    {
        Swat = GameObject.FindGameObjectsWithTag("Swat");
        nbEnnemies = Swat.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Swat = GameObject.FindGameObjectsWithTag("Swat");
        nbEnnemies = Swat.Length;
        GameObject.Find("TxtNb").GetComponent<Text>().text = "En Vie:" + nbEnnemies;

        if (nbEnnemies == 0)
        {
            GameObject.Find("TxtNb").GetComponent<Text>().text = "Bien maintenant elimine le Boss qui se trouve dans la vieille armurie sovietique...";
        }
    }
}
