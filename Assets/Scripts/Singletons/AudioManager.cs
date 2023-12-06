using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Inscribed")]
    public List<AudioSource> HardcodedSFX = new List<AudioSource>();//predetermained sounds
    public AudioSource FlexibleSFX;//audio source for passing in audio clips
    // Start is called before the first frame update
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this); //retain between scenes
    }

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
