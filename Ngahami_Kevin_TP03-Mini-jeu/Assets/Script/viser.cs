using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viser : MonoBehaviour
{
    public Vector3 normalPos;
    public Vector3 aimPos;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = normalPos;
    }

    // Update is called once per frame
    void Update()
    {
        //Nous permet de changer la position de notre arme
        if (Input.GetButtonDown("Fire2"))
        {
            transform.localPosition = aimPos;
        }

        if (!Input.GetButtonDown("Fire2"))
        {
            transform.localPosition = normalPos;
        }
    }
}
