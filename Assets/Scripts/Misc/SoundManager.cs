using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {

    public enum Sound{
        PlayerAttack,
        EnemyAttack,
        PlayerMove
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    public static void Initialize(){
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerAttack] = 0f;
        soundTimerDictionary[Sound.EnemyAttack] = 0f;
    }

    public static void PlaySound(Sound sound, Vector3 position){
        if (CanPlaySound(sound)){
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = (GetAudioClip(sound));
            audioSource.maxDistance = 50f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();
        }
    }

    public static void PlaySound(Sound sound){
        if (CanPlaySound(sound)){
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static bool CanPlaySound(Sound sound){
        switch (sound){
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound)){
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerAttackTimerMax = 0.05f;
                    if (lastTimePlayed + playerAttackTimerMax < Time.time){
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else{
                        return false;
                    }
                }
                else{
                    return true;
                }
        }
    }

    public static AudioClip GetAudioClip(Sound sound){
        foreach (UIManager.SoundAudioClip soundAudioClip in UIManager.instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
