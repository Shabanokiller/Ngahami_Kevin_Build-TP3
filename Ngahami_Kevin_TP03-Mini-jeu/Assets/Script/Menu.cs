using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button btnJouer;
    public Button btnInstructions;
    public Button btnSuivant;
    public Button btnExit;
    public GameObject Panel;
    public GameObject instrut;
    private bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        btnJouer.onClick.AddListener(btnJouer_Clicked);
        btnInstructions.onClick.AddListener(btnInstructions_Clicked);
        btnSuivant.onClick.AddListener(btnSuivant_Clicked);
        btnExit.onClick.AddListener(btnExit_Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void btnJouer_Clicked()
    {
        // Affiche la scene du jeu
        SceneManager.LoadScene("Main");

    }

    void btnInstructions_Clicked()
    {
        // Affiche la scene du jeu
        visible = !visible;
        Panel.SetActive(visible);

    }

    void btnSuivant_Clicked()
    {
        // Affiche la scene du jeu
        visible = !visible;
        instrut.SetActive(visible);
        Panel.SetActive(!visible);

    }

    void btnExit_Clicked()
    {
        // On sort de nos differents panels pour revenir a la scene acceuil jeu
        //visible = !visible;
        instrut.SetActive(!visible);
        Panel.SetActive(!visible);

    }
}
