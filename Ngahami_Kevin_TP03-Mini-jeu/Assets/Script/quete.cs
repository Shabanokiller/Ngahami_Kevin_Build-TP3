using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quete : MonoBehaviour
{

    private bool GUIText = false;
    public int objectAcquire;
    public GameObject object1;
    public GameObject object2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jiuer ");
            GUIText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GUIText = false;
        }
    }

    private void OnGUI()
    {
        if(GUIText == true)
        {
            if(objectAcquire == 2)
            {
                Destroy(gameObject);
            }
        }
        Cursor.visible = true;
        //Rect Rectangle = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150);
        GUI.BeginGroup(new Rect(Screen.width/2 - 150, Screen.height/2 - 75,300,150));
        GUI.Box(Rect(0, 0, 300, 150), "Nouvelle Sous-Quete");
        GUI.Label(Rect(10, 20, 200, 300), "Bonjour Agent Wick, nous vennons d'apprendre a l'aide de nos satellites, que SCH mene des activites sur la necromancie, nous voulons savoir si vous pouvez verifier cela?");

        if(GUI.Button(Rect(10, 90, 60, 50), "Accepter"))
        {
            object1.SetActive(true);
            object2.SetActive(true);
            GUIText = false;
        }

        if (GUI.Button(Rect(230, 90, 60, 50), "Refuser"))
        {
            GUIText = false;
            Cursor.visible = false;
        }

        GUI.EndGroup();
    }

    private Rect Rect(int v1, int v2, int v3, int v4)
    {
        return Rect(0, 0, 300, 150);
    }
}
