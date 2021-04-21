using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    RbCharacterMovements rb;
    public PlayableDirector playableDirector;
    private CameraPositioner cameraposition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("ely_k_atienza").GetComponent<RbCharacterMovements>();
        cameraposition = GameObject.Find("ely_k_atienza").GetComponent<CameraPositioner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.enabled = false;
            cameraposition.enabled = false;
            Play();

        }
    }

    public void Play()
    {
        playableDirector.Play();
    }
}
