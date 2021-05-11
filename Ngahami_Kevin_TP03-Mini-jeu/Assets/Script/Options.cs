using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject Panel;
    private bool visible = false;
    private bool pause = false;
    private bool actif = false;
    RbCharacterMovements rb;

    //public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = FindObjectOfType<RbCharacterMovements>();
    }

    // Update is called once per frame
    void Update()
    {
        if(actif == true)
        {
            Panel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            //Time.timeScale = 0;
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause)
            {
                visible = !visible;
                Panel.SetActive(visible);
                Resume();
                rb.enabled = true;
            }
            else
            {
                visible = !visible;
                Panel.SetActive(visible);
                Pause();
                rb.enabled = false;
            }
            
        }
        //
    }

    public void Pause()
    {
        //pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
        ///pause = !pause;
    }

    public void Resume()
    {
        //pauseMenu.SetActive(true);
        Time.timeScale = 1f;
        pause = false;
        ///pause = !pause;
    }

    //public void EndGame()
    //{
    //    GameOver.SetActive(true);
    //    Time.timeScale = 0f;
    //    end = true;
    //    ///pause = !pause;
    //}
}
