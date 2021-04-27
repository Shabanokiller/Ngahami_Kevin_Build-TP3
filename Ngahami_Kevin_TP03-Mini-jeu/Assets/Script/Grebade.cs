using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grebade : MonoBehaviour
{
    private PlayerStat playerStat;
    public Rigidbody grenade;
    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = GameObject.Find("ely_k_atienza").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        //Nous permet de verifier le nombre de grenade qu'on a en stock
        if(playerStat.grenades >= 1)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Rigidbody grean;
                grean = Instantiate(grenade, transform.position, transform.rotation);
                grean.velocity = transform.TransformDirection(Vector3.forward * speed);
                playerStat.grenades -= 1;
            }
        }
    }
}
