using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int bulletLeft = 310;
    private int bulletPerMag = 31;
    private int currentBullets;
    public float reloadTime = 3f;
    private float fireRate = 0.1f;
    private float range = 100f;
    float fireTimer;
    public Transform barrelEnd;
    public Transform shootpoint;
    public ParticleSystem etincele;
    private AudioSource audioSource;
    private AudioSource _audioSource;
    public AudioClip shootSound;
    public AudioClip deadSound;
    public GameObject hitParticule;
    public GameObject muzzleFlash;
    private RaycastHit hit;
    bool isReloading = false;


    // Start is called before the first frame update
    void Start()
    {
        currentBullets = bulletPerMag;
        audioSource = GetComponent<AudioSource>();
        //hitParticule.SetActive(false);
        muzzleFlash.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Pour effectuer un tir
        if (Input.GetButtonDown("Fire1"))
        {


            if(currentBullets > 0)
            {
                Fire();
            }
            // Je fais un rayon a partir de BarrelEnd
            Ray bulletRay = new Ray(barrelEnd.position, barrelEnd.forward);
            // Si le rayon impacte sur un object, on le propulse
            if (Physics.Raycast(bulletRay, out hit))
            {
                // Ragdoll?
                Ragdoll ragdoll = hit.collider.GetComponentInParent<Ragdoll>();
                if (ragdoll != null)
                {
                    ragdoll.die = true;
                    Debug.Log("L'ennemie a ete touche");
                    PlayDeadSound();
                }
            }
            
        }

        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime; 
        }

        
    }
    // fonction pour effectuer un tir 
    private void Fire()
    {
        if (fireTimer < fireRate || currentBullets <= 0)
        {
            return;
        }
           
        currentBullets--;
        Debug.Log("Balles restantes : " + currentBullets);
        if (currentBullets <= 0)
        {
            Reload();
            return;
        }


        RaycastHit hit;

        //// Verification de la distance
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        //if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
        //{
        //    //if(hit.distance <= dis)

        //    // nous permet de faire voler nos barril lorsq'on tire dessus
        //    if (hit.transform.gameObject.tag == "Barril")
        //    {
        //        hit.rigidbody.AddForceAtPosition(transform.TransformDirection(Vector3.forward) * 1000, hit.normal);
        //    }
        //    Debug.Log("Touche : " + hit.transform.name);
        //}

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
        {
            // nous permet de tuer notre ennemie lorsq'on tire dessus
            if (hit.transform.gameObject.tag == "swat")
            {
                GameObject.Find(hit.transform.name).GetComponent<AiSwat>().SwatDead();
            }
        }

        // nous permet de faire sortir nos etincelles lors de l'impsct
        if (Physics.Raycast(shootpoint.position, shootpoint.transform.forward, out hit, range))
        {
            GameObject hitParticules = Instantiate(hitParticule, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(hitParticules, 5f);
        }


        if (!Input.GetKey(KeyCode.LeftShift))
        {
            etincele.Play();
            PlayShootSound();
            muzzleFlash.SetActive(true);
            StartCoroutine(wait());
        }

        fireTimer = 0.0f;
    }
    // fonction pour jouer le son de tir de fusil
    private void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
    // fonction pour jouer le son de mort 
    private void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadSound);
    }
    // Le delais d'attente pour notre muzzle flash
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
    }
    // La fonction qui nous permet d'effectuer notre recharge d'arme
    public void Reload() 
    {
        if(isReloading == true)
        {
            return;
        }
        StartCoroutine(Reload_Coroutine());
    }
    // Notre coroutine pour le rechargement de notre arme 
    public IEnumerator Reload_Coroutine()
    {

        Debug.Log("Reloading....");
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentBullets = bulletPerMag;

        isReloading = false;
        Debug.Log("Finish Reloading...");
    }
}
