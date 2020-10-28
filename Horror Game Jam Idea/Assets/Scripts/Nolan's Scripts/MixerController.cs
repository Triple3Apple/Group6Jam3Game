using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;

    public void SetMaster(float volumeLevel)
    {
        mainMixer.SetFloat("masterVol", volumeLevel);
    }

    public void SetMusic(float volumeLevel)
    {
        mainMixer.SetFloat("musicVol", volumeLevel);
    }

    public void SetSFX(float volumeLevel)
    {
        mainMixer.SetFloat("sfxVol", volumeLevel);
    }
}
