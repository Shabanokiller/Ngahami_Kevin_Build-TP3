using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public AudioClip emptySound;
    public GameObject hitParticule;
    public GameObject muzzleFlash;
    private RaycastHit hit;
    bool isReloading = false;
    private Animator animatorEly;
    WeaponManager weaponManager;
    RbCharacterMovements rb;
    private int Score = 0;
    private GameObject[] swat;
    private Ray ray;


    // Start is called before the first frame update
    void Start()
    {
        currentBullets = bulletPerMag;
        audioSource = GetComponent<AudioSource>();
        //hitParticule.SetActive(false);
        muzzleFlash.SetActive(false);
        animatorEly = GetComponent<Animator>();
        weaponManager = FindObjectOfType<WeaponManager>();
        rb = FindObjectOfType<RbCharacterMovements>();
        //TxtScore = GameObject.Find("TxtScore").GetComponent<Text>();
        swat = GameObject.FindGameObjectsWithTag("Swat");
    }

    // Update is called once per frame
    void Update()
    {
        //Pour effectuer un tir
        if(Input.GetButtonDown("Fire1"))
        {
            //On centre a l'ecran
            //Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            //ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);
            //if(Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            //{
            //    if (hit.transform.gameObject.CompareTag("Swat"))
            //    {
            //        Score += 100;
            //        TxtScore.text = "Score : " + Score;
            //        //GameObject.Find
            //        //GameObject.Find(hit.transform.name).GetComponent<AiSwat>().SwatDead();
            //    }
            //}

            //animatorEly.SetBool("Shoot", true);
            if(currentBullets > 0)
            {
                //rb.Feu();
                Fire();
            }
            //else {
            //    rb.AnnulerFeu();
            //}
            // Je fais un rayon a partir de BarrelEnd
            Ray bulletRay = new Ray(barrelEnd.position, barrelEnd.forward);
            // Si le rayon impacte sur un object, on le propulse
            if (Physics.Raycast(bulletRay, out hit))
            {
                //if (hit.transform.gameObject.CompareTag("Swat"))
                //{
                //    Score += 100;
                //    TxtScore.text = "Score : " + Score;
                //    //GameObject.Find
                //    //GameObject.Find(hit.transform.name).GetComponent<AiSwat>().SwatDead();
                //}

                // Ragdoll?
                Ragdoll ragdoll = hit.collider.GetComponentInParent<Ragdoll>();
                if (ragdoll != null)
                {
                    swat = GameObject.FindGameObjectsWithTag("Swat");
                    Score += 100;
                    GameObject.Find("TxtScore").GetComponent<Text>().text = "Score:" + Score;
                    ragdoll.die = true;
                    Debug.Log("L'ennemie a ete touche");
                    PlayDeadSound();
                }
            }
            
        }
        //else
        //{
        //    rb.AnnulerFeu();
        //}

        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime; 
        }

        
    }
    // fonction pour effectuer un tir 
    public void Fire()
    {
        //animatorEly.SetBool("Shoot", true);
        //rb.Feu();
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
            if (hit.transform.gameObject.tag == "Swat")
            {
                Score += 100;
                //TxtScore.text = "Score : " + Score;
                //GameObject.Find
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
        PlayEmptySound();
        weaponManager.enabled = false;
        yield return new WaitForSeconds(reloadTime);
        currentBullets = bulletPerMag;

        isReloading = false;
        Debug.Log("Finish Reloading...");
        weaponManager.enabled = true;
    }
    // fonction pour jouer le son de chargeur vide 
    private void PlayEmptySound()
    {
        audioSource.PlayOneShot(emptySound);
    }

    //private bool MouseOverUI()
    //{
    //    PointerEventData eventData = new PointerEventData(EventSystem.current);
    //    eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventData, results);
    //    return results.Count > 0;
    //}
}
