using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [Header("Inscribed")]
    public List<AudioSource> HardcodedSFX = new List<AudioSource>();//predetermained sounds
    public AudioSource FlexibleSFX;//audio source for passing in audio clips
    // Start is called before the first frame update

    //play a preallocated sound
    public void PlaySound(int id)
    {
        Debug.Log("Playing a Sound");
        if (id < HardcodedSFX.Count)
            HardcodedSFX[id].Play();
    }
    //play a sound clip passed through a function
    public void PlaySound(AudioClip sound)
    {
        Debug.Log("Playing a Sound");
        FlexibleSFX.clip = sound;
        FlexibleSFX.Play();
    }
}
