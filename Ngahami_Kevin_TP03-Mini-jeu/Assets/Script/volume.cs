using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class volume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeAmbiance;
    public Slider volumeFusil;
    public Slider volumeMort;
    // Start is called before the first frame update
    void Start()
    {
        volumeAmbiance.onValueChanged.AddListener(volumeAmbiance_OnValueChanged);
        volumeFusil.onValueChanged.AddListener(volumeFusil_OnValueChanged);
        volumeMort.onValueChanged.AddListener(volumeMort_OnValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void volumeAmbiance_OnValueChanged(float value)
    {
        mixer.SetFloat("MusicAmbiance", Mathf.Log(value) * 20f);
    }

    void volumeFusil_OnValueChanged(float value)
    {

    }

    void volumeMort_OnValueChanged(float value)
    {

    }
}
