using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float currentHP = 100f;
    public float maxHP = 100f;
    public float hpBarLenght;
    public float pourcentageDeHp;
    //public Texture2D hpBarTexture;

    void OnGUI()
    {
        // Nous permet de situer notre bar de vie dans notre ecran
        if (currentHP > 0)
        {
            //GUI.DrawTexture(Rect(10, 10, hpBarLenght, 10), hpBarTexture);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pourcentageDeHp = currentHP / maxHP;
        hpBarLenght = pourcentageDeHp * 100;

        // Nous permet de nous regenrer apres un certains moments
        currentHP += Time.deltaTime * 2;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    
}
