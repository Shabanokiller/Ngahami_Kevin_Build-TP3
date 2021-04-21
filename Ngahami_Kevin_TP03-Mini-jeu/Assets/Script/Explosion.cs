using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int tempsExplosion = 10;
    public GameObject zoneExplosion;

    // Start is called before the first frame update
    void Start()
    {
        wait();
        apparaitre();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Le delais d'attente pour notre la bombe
    IEnumerator wait()
    {
        yield return new WaitForSeconds(tempsExplosion);
    }

    void apparaitre()
    {
        Instantiate(zoneExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
