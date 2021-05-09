using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public int grenades = 50;
    public float healthBar = 100f;
    public float healthMax = 100f;
    public float tempsDeRegeneration = 0f;
    public GameObject Ui;
    private Rigidbody rb;
    public Image health;
    private Animator animatorEly;
    RbCharacterMovements rbCharacter;
    public GameObject GameOver;
    private bool visible = false;
    private bool end = false;
    //private CameraPositioner cameraposition;


    private void Awake()
    {
        // Nous permet d'invvoqurer notre timer et notre regeneration
        InvokeRepeating("Regeneration", 0, 1);
        InvokeRepeating("Timer", 0, 1);
    }
    

    void Start()
    {
        // Assigner le Rigidbody
        rb = GetComponent<Rigidbody>();

        //Assigner l'animator
        animatorEly = GetComponent<Animator>();

        //on instantie le RbCharacterMovements 
        rbCharacter = GameObject.Find("ely_k_atienza").GetComponent<RbCharacterMovements>();

        //cameraposition = GameObject.Find("ely_k_atienza").GetComponent<CameraPositioner>();

        healthBar = healthMax;

        //options = GameObject.Find("ely_k_atienza").GetComponent<Options>();
    }

    private void Update()
    {
        health.fillAmount = healthBar / healthMax;

        // Nous permet de diminuer progressivement notre image a l'ecran dependament de l'interval
        //if(healthBar == 0)
        //{
        //    visible = !visible;
        //    GameOver.SetActive(true);
        //    Time.timeScale = 0f;
        //    end = true;
        //}
        if (healthBar >= 1 && healthBar < 20)
        {
            Ui.GetComponent<CanvasGroup>().alpha = 1;
        }

        if (healthBar >= 20  && healthBar < 60)
        {
            Ui.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        if (healthBar >= 60 && healthBar < 80)
        {
            Ui.GetComponent<CanvasGroup>().alpha = 0.3f;
        }

        if (healthBar >= 80 && healthBar <= 100)
        {
            Ui.GetComponent<CanvasGroup>().alpha = 0f;
        }

        // Nous permet de bloquer notre variable de vie dans un interval
        if (healthBar > 100)
        {
            healthBar = 100;
        }

        // Nous permet de bloquer notre variable de temps dans un interval
        if(tempsDeRegeneration > 5)
        {
            tempsDeRegeneration = 5;
        }
        if (tempsDeRegeneration <= 0)
        {
            tempsDeRegeneration = 0;
        }
        //EndGame();
    }

    // Nous permet d'appliquer les dommages a notre joueur
    public void Dommage(int degats)
    {
        healthBar -= degats;
        tempsDeRegeneration += 2;

        if(healthBar <= 0)
        {
            Dead();
            //options.EndGame();
        }
    }

    public void EndGame()
    {
        //GameOver.SetActive(true);
        Time.timeScale = 0f;
        end = true;
        ///pause = !pause;
    }

    public void Dead()
    {
        Debug.Log("Player Dead");
        //animatorEly.
        //animatorEly.enabled = false;
        rbCharacter.enabled = false;
        //cameraposition.enabled = false;
        animatorEly.SetBool("Dead", true);
        visible = !visible;
        GameOver.SetActive(visible);
        EndGame();
    }

    // On ajoute +1 a chque fois a healthbar
    void Regeneration()
    {
        if(tempsDeRegeneration == 0)
        {
            healthBar += 1;
        }
    }

    // fonction pour notre temps de regeneration
    void Timer()
    {
        tempsDeRegeneration -= 1;
    }
}
