using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip powerDownSound;
    [SerializeField] private AudioClip powerUpSound;

    private float initialVolume;

    private AudioSource audioManSource;

    [SerializeField] private AnimationCurve powerdownSoundCurve;
    private void Start()
    {
        audioManSource = GetComponent<AudioSource>();
        initialVolume = audioManSource.volume;
    }

    public void PlayPowerDownSound()
    {
        audioManSource.volume = 0f;
        audioManSource.DOFade(initialVolume, powerDownSound.length).SetEase(powerdownSoundCurve);
        audioManSource.PlayOneShot(powerDownSound);
    }

    public void PlayPowerUpSound()
    {
        Debug.Log("Played Power Up sound");
        audioManSource.volume = 0f;
        audioManSource.DOFade(initialVolume, powerUpSound.length).SetEase(powerdownSoundCurve);
        audioManSource.PlayOneShot(powerUpSound);
    }

}
