using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class slider : MonoBehaviour
{

    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // function nous permettant de modifie notre slider
    public void SetVolume(float sliderValue)
    {
        Debug.Log("Bien");
        mixer.SetFloat("MusicAmbiance", Mathf.Log(sliderValue) * 20f);
    }
}
