using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clips : MonoBehaviour {

    public float lowPitchRange = .9f;
    public float highPitchRange = 1.2f;

    public AudioClip walking;
    public AudioClip catAttack;
    public AudioClip catDies;
    public AudioClip bearAttacks;
    public AudioClip bearFollows;
    public AudioClip bearMoans;

    public AudioSource walkingSource;
    public AudioSource catAttackSource;
    public AudioSource catDieSource;
    public AudioSource bearAttacksSource;
    public AudioSource bearFollowsSource;
    public AudioSource bearMoansSource;

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.volume = vol;
        return newAudio;
    }
     

    public void Awake()
    {
        // add the necessary AudioSources:
        walkingSource = AddAudio(walking, true, true, 0.2f);
        catAttackSource = AddAudio(catAttack, false, true, 0.2f);
        catDieSource = AddAudio(catDies, false, true, 0.2f);
        bearAttacksSource = AddAudio(bearAttacks, false, true, 0.2f);
        bearFollowsSource = AddAudio(bearFollows, true, true, 0.2f);
        bearMoansSource = AddAudio(bearMoans, false, true, 0.2f);
    }

    public void RandomPitchPlay(AudioSource source)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        source.pitch = randomPitch;
        source.Play();
    }

    public void RandomSourceAndPitchPlay(AudioSource[] sources)
    {
        int randomIndex = Random.Range(0, sources.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        var source = sources[randomIndex];
        source.pitch = randomPitch;
        source.Play();
    }

}
