using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("ely_k_atienza").GetComponent<HealthBar>().currentHP -= GameObject.Find("swat").GetComponent<AiSwat>().Degats;
        }
    }
}
