using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button btnJouer;

    // Start is called before the first frame update
    void Start()
    {
        btnJouer.onClick.AddListener(btnJouer_Clicked);
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
}
