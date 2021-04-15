using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimperEchell : MonoBehaviour
{
    public bool Monter;
    public float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Monter)
        {
            GetComponent<CharacterController>().transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Ladder")
        {
            Debug.Log("OK");
            GetComponent<RbCharacterMovements>().enabled = false;
            Monter = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            GetComponent<RbCharacterMovements>().enabled = true;
            GetComponent<Rigidbody>().transform.Translate(transform.TransformDirection(Vector3.forward));
            Monter = false;
        }
    }

    
}
