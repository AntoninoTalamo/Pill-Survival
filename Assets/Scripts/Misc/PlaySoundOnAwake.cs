using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour
{
    public int SoundToPlay = 0;
    private void OnEnable()
    {
        AudioManager.Instance.PlaySound(0);
    }
}
