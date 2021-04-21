using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatHealth : MonoBehaviour
{
    public int swatHealth = 100;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(swatHealth <= 0)
        {
            anim.SetBool("Dead", true);
            gameObject.GetComponent<AiSwat>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            swatHealth -= 25;
            Debug.Log("ennemie touchee");
        }
    }
}
