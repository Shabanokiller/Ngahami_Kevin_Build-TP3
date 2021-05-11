using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    //Nous permet de loader  notre scene
    void OnEnable()
    {
        //Seul spécifier le nom de scène ou sceneBuildIndex chargera la scène avec le mode Unique
        SceneManager.LoadScene(3);
    }
}
