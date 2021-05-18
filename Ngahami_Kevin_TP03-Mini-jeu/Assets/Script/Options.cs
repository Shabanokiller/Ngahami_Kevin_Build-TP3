using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour
{
    public GameObject Panel;
    private bool visible = false;
    private bool pause = false;
    private bool actif = false;
    RbCharacterMovements rb;
    CameraPositioner positioner;
    WeaponManager weaponManager;
    WeaponManager weaponManager1;

    //public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = FindObjectOfType<RbCharacterMovements>();
        positioner = FindObjectOfType<CameraPositioner>();
        weaponManager = GameObject.Find("SciFiGunLightBlack").GetComponent<WeaponManager>();
        weaponManager1 = GameObject.Find("AK74").GetComponent<WeaponManager>();
        Cursor.lockState = CursorLockMode.Locked;
        //weaponManager = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(actif == true)
        //{
        //    Panel.SetActive(true);
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.Confined;
        //    //Time.timeScale = 0;
        //}

        // Nous permet d'activer le menu options
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause)
            {
                Cursor.lockState = CursorLockMode.Locked;
                visible = !visible;
                Panel.SetActive(visible);
                Resume();
                rb.enabled = true;
                positioner.enabled = true;
                weaponManager.enabled = true;
                weaponManager1.enabled = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                visible = !visible;
                Panel.SetActive(visible);
                Pause();
                rb.enabled = false;
                positioner.enabled = false;
                weaponManager.enabled = false;
                weaponManager1.enabled = false;
            }
            
        }
        //
    }

    public void Pause()
    {
        //pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
        //weaponManager.enabled = false;
        ///pause = !pause;
    }

    public void Resume()
    {
        //pauseMenu.SetActive(true);
        Time.timeScale = 1f;
        pause = false;
        //weaponManager.enabled = true;
        ///pause = !pause;
    }

    //public void EndGame()
    //{
    //    GameOver.SetActive(true);
    //    Time.timeScale = 0f;
    //    end = true;
    //    ///pause = !pause;
    //}
    //private bool MouseOverUI()
    //{
    //    return EventSystem.current.IsPointerOverGameObject();
    //}
}
