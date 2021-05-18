using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quete : MonoBehaviour
{
    public GameObject Quete;
    private bool GUIText = false;
    private bool pause = false;
    public int objectAcquire;
    public GameObject object1;
    public GameObject object2;
    RbCharacterMovements rb;
    CameraPositioner positioner;
    WeaponManager weaponManager; 
    public Button btnAccepeter;
    public Button btnRefuser;

    // Start is called before the first frame update
    void Start()
    {
        rb = FindObjectOfType<RbCharacterMovements>();
        positioner = FindObjectOfType<CameraPositioner>();
        weaponManager = FindObjectOfType<WeaponManager>();
        btnAccepeter.onClick.AddListener(btnAccepter_OnClick);
        btnRefuser.onClick.AddListener(btnRefuser_OnClick);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Nous permet de declancher notre mission lorsqu'on entre en collision le trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Quete.SetActive(true);
            StartCoroutine("WaitForSec");
            Debug.Log("Jiuer ");
            GUIText = true;
            //Pause();
            //rb.enabled = true;
            //positioner.enabled = true;
            //weaponManager.enabled = true;
        }
    }
    //Nous permet d'effacer notre mission lorsqu'on sort en collision le trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Quete.SetActive(false);
            GUIText = false;
            //Resume();
            //rb.enabled = false;
            //positioner.enabled = false;
            //weaponManager.enabled = false;
        }
    }
    //Bouton permettant d'accepter notre mission
    void btnAccepter_OnClick()
    {
        object1.SetActive(true);
        object2.SetActive(true);
        Quete.SetActive(false);
        rb.enabled = true;
        positioner.enabled = true;
        weaponManager.enabled = true;
    }
    //Bouton permettant de refuser notre mission
    void btnRefuser_OnClick()
    {
        Quete.SetActive(false);
        rb.enabled = true;
        positioner.enabled = true;
        weaponManager.enabled = true;
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(6);
        Destroy(Quete);
        Destroy(gameObject);
    }
}
