﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                    
    public AudioSource musicSource;                  
    public static SoundManager self = null;     
    public float lowPitchRange = .95f;              
    public float highPitchRange = 1.05f;            


    void Awake()
    {
       
		if (self == null)
            self = this;
        else if (self != this)
            Destroy(gameObject);
  
        DontDestroyOnLoad(gameObject);
    }


	public void PlayBGM(AudioClip clip)
    {      
		musicSource.clip = clip;

		musicSource.Stop();
		musicSource.Play();
    }


    public void PlaySingle(AudioClip clip)
    {
  
        efxSource.clip = clip;
  
		efxSource.Stop();
        efxSource.Play();
    }


  
    public void RandomizeSfx(params AudioClip[] clips)
    {
  
        int randomIndex = Random.Range(0, clips.Length);
  
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;

  
        efxSource.clip = clips[randomIndex];

  
        efxSource.Play();
    }
}
