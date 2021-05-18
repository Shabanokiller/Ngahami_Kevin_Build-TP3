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
    public Slider volumeFusilEnnemie;
    public Slider volumeExplosion;
    RbCharacterMovements rb;
    CameraPositioner positioner;
    WeaponManager weaponManager;
    //public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        volumeAmbiance.onValueChanged.AddListener(volumeAmbiance_OnValueChanged);
        volumeFusil.onValueChanged.AddListener(volumeFusil_OnValueChanged);
        volumeFusilEnnemie.onValueChanged.AddListener(volumeFusilEnnemie_OnValueChanged);
        volumeExplosion.onValueChanged.AddListener(volumeBombe_OnValueChanged);
        rb = FindObjectOfType<RbCharacterMovements>();
        positioner = FindObjectOfType<CameraPositioner>();
        weaponManager = FindObjectOfType<WeaponManager>();
        //slider.onValueChanged.AddListener(volumeSlider_OnValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
    }
    // function nous permettant de modifie notre slider
    void volumeAmbiance_OnValueChanged(float value)
    {
        mixer.SetFloat("MusicAmbiance", Mathf.Log(value) * 20f);
    }
    // function nous permettant de modifie notre slider
    void volumeFusil_OnValueChanged(float value)
    {
        mixer.SetFloat("MusicFusil", Mathf.Log(value) * 20f);
    }
    // function nous permettant de modifie notre slider
    void volumeFusilEnnemie_OnValueChanged(float value)
    {
        mixer.SetFloat("MusicEnnemies", Mathf.Log(value) * 20f);
    }
    void volumeBombe_OnValueChanged(float value)
    {
        mixer.SetFloat("MusicGrenade", Mathf.Log(value) * 20f);
    }

    //void volumeSlider_OnValueChanged(float value)
    //{
    //    Debug.Log("slider : " + value);
    //}
}
