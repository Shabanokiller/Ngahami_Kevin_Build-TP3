using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feu : MonoBehaviour
{
    public Transform barrelEnd;
    public LineRenderer bulletLine;
    //Pour definir un son sur nos boutons
    public AudioClip audioClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // Si je fais feu
        if (Input.GetMouseButtonDown(0))
        {
            

            // Je fais un rayon a partir de BarrelEnd
            Ray bulletRay = new Ray(barrelEnd.position, barrelEnd.forward);

            RaycastHit hit;

            bulletLine.SetPosition(0, barrelEnd.position);

            // Si le rayon impacte sur un object, on le propulse
            if (Physics.Raycast(bulletRay, out hit))
            {
                //PointB du LineRenderer (tir reussi)
                bulletLine.SetPosition(1, hit.point);
                Explode(hit.point);

                // Ragdoll?
                Ragdoll ragdoll = hit.collider.GetComponentInParent<Ragdoll>();
                if (ragdoll != null) 
                {
                    ragdoll.die = true;
                }
            }
            else
            {
                //PointB du LineRenderer (tir rate)
                bulletLine.SetPosition(1, barrelEnd.position + barrelEnd.forward * 20f);
            }
        }
    }

    void Explode(Vector3 impactPosition)
    {
        // Recuperer tous les objects qui sont dans le permiterer de l'explosion
        Collider[] colliders = Physics.OverlapSphere(impactPosition, 5f);

        // Pour chaque object recupere, je lui donne une velocite
        foreach(Collider coll in colliders)
        {
            Rigidbody rb = coll.GetComponent<Rigidbody>();

            //Si le rigidbody a ete trouve
            if(rb != null)
            {
                rb.AddExplosionForce(5f, impactPosition, 5f, 1f, ForceMode.Impulse);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(barrelEnd.position, barrelEnd.forward * 20f);
    //}
}
