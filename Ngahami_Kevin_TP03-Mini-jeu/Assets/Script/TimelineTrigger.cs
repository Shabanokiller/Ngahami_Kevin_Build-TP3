using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineTrigger : MonoBehaviour
{
    RbCharacterMovements rb;
    WeaponManager weaponManager;
    private int bulletLeft = 310;
    private int bulletPerMag = 31;
    private int currentBullets;
    public float reloadTime = 3f;
    bool isReloading = false;
    public PlayableDirector playableDirector;
    //private CameraPositioner cameraposition;

    // Start is called before the first frame update
    void Start()
    {
        rb = FindObjectOfType<RbCharacterMovements>();
        weaponManager = FindObjectOfType<WeaponManager>();
        //cameraposition = GameObject.Find("ely_k_atienza").GetComponent<CameraPositioner>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.enabled = false;
            //Reload();
            //weaponManager.enabled = false;
            //cameraposition.enabled = false;
            Play();

        }

        //SceneManager.LoadScene(3);
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        rb.enabled = false;
    //        //cameraposition.enabled = false;
    //        SceneManager.LoadScene(3);

    //    }
    //}

    public void Play()
    {
        playableDirector.Play();
        weaponManager.enabled = false;
    }

    // La fonction qui nous permet d'effectuer notre recharge d'arme
    public void Reload()
    {
        if (isReloading == true)
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
        //PlayEmptySound();
        yield return new WaitForSeconds(reloadTime);
        currentBullets = bulletPerMag;

        isReloading = false;
        Debug.Log("Finish Reloading...");
    }
}
