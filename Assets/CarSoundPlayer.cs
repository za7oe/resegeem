using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundPlayer : MonoBehaviour

{
    public CarController1 controller;
    public AudioSource audioSource;
    public AudioClip bumpSE;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.InverseLerp(1877, 7500,controller.rpm);
        audioSource.pitch = Mathf.Lerp(0.2f, 1f, t);



    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall") 
        {
            audioSource.PlayOneShot(bumpSE);

        }
    }
}
