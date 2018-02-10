using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public Clips clips;
    public AudioSource efxSource;
    public AudioSource walkSource;
    public AudioSource musicSource;
    //public static SoundManager instance = null;
    public float lowPitchRange = .5f;
    public float highPitchRange = 1.5f;



    //void Awake()
    //{
    //    if (instance == null)
    //        instance = this;
    //    else if (instance != this)
    //        Destroy(gameObject);

    //    DontDestroyOnLoad(gameObject);

    //}


    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void StopSingle(AudioClip clip)
    {

    }

    /*public void PlayWhileTriggered (AudioClip clip)
    {

    }*/



    //  SoundManager.instance.RandomizeSfx(pickupSound1, pickupSound2, pickupSound3, pickupSound4, pickupSound5, pickupSound6);

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
