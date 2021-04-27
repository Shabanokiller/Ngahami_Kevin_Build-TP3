using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public GameObject destruction;

    public void Destroy()
    {
        Instantiate(destruction, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
