using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button btnJouer;
    public Button btnInstructions;
    public GameObject Panel;
    private bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        btnJouer.onClick.AddListener(btnJouer_Clicked);
        btnInstructions.onClick.AddListener(btnInstructions_Clicked);
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
}
