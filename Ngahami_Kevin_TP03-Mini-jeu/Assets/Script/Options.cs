using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject Panel;
    private bool visible = false;
    private bool pause = false;
    //public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause)
            {
                visible = !visible;
                Panel.SetActive(visible);
                Resume();
            }
            else
            {
                visible = !visible;
                Panel.SetActive(visible);
                Pause();
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
}
