using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    public int playerHealth = 100;
    public int degats = 40;
    private PlayerStat playerStat;
    public AudioClip audio;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = GameObject.Find("ely_k_atienza").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            Debug.Log("player touche");
            playerStat.Dommage(degats);
            //playerStat.Dead();
            //GetComponent<PlayerStat>().Dommage(degats);
            GetComponent<AudioSource>().PlayOneShot(audio);
            //GameObject.Find("ely_k_atienza").GetComponent<HealthBar>().currentHP -= GameObject.Find("swat").GetComponent<AiSwat>().Degats;
        }
    }
}
