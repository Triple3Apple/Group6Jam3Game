using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip powerDownSound;
    [SerializeField] private AudioClip powerUpSound;
    [SerializeField] private AudioClip lightOffAmbiance;

    private float initialVolume;

    private AudioSource audioManSource;

    [SerializeField] private AnimationCurve powerdownSoundCurve;

    [SerializeField] private AnimationCurve ambianceSoundCurve;
    private void Start()
    {
        audioManSource = GetComponent<AudioSource>();
        initialVolume = audioManSource.volume;
    }

    public void PlayPowerDownSound(float lightsOffDuration)
    {
        audioManSource.volume = 0f;
        audioManSource.DOFade(initialVolume, powerDownSound.length).SetEase(powerdownSoundCurve).OnComplete(() => PlayLightsOffAmbiance(lightsOffDuration));
        audioManSource.PlayOneShot(powerDownSound);
    }

    public void PlayPowerUpSound()
    {
        audioManSource.Stop();
        Debug.Log("Played Power Up sound");
        audioManSource.volume = 0f;
        // ()=>ResetAudioVolume(initClockVolume)
        audioManSource.DOFade(initialVolume, powerUpSound.length).SetEase(powerdownSoundCurve);
        audioManSource.PlayOneShot(powerUpSound);
    }

    public void PlayLightsOffAmbiance(float lightsOffTime)
    {
        audioManSource.DOFade(initialVolume, powerUpSound.length).SetEase(ambianceSoundCurve);
        audioManSource.PlayOneShot(lightOffAmbiance);
    }

}
