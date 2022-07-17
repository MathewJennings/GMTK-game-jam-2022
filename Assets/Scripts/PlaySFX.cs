using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>().PlayOneShot(sound, 0.5f);
    }
}
