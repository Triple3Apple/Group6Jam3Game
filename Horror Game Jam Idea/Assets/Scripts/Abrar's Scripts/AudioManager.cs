using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Level Sounds")]
    [SerializeField] private AudioClip powerDownSound;
    [SerializeField] private AudioClip powerUpSound;
    [SerializeField] private AudioClip lightOffAmbiance;

    [Header("Chasing and Scare Sounds")]
    [SerializeField] private AudioClip chaseMusic;
    [SerializeField] private AudioClip caughtSound;
    [SerializeField] private AudioSource chaseMusicAudioSource;
    [SerializeField] private AudioSource caughtAudioSource;
    [SerializeField] private float chaseVolume = 0.3f;
    [SerializeField] private float caughtVolume = 0.3f;


    private float initialVolume;

    private AudioSource audioManSource;

    [SerializeField] private AnimationCurve powerdownSoundCurve;

    [SerializeField] private AnimationCurve ambianceSoundCurve;

    [SerializeField] private AudioSource easterEggScreamAudioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayCreepyScream();
        }
    }

    private void Start()
    {
        audioManSource = GetComponent<AudioSource>();
        initialVolume = audioManSource.volume;
        chaseMusicAudioSource.volume = 0.3f;
        chaseVolume = chaseMusicAudioSource.volume;
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
        //Debug.Log("Played Power Up sound");
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

    // play chase music
    public void PlayChaseMusic()
    {
        chaseMusicAudioSource.volume = chaseVolume;
        chaseMusicAudioSource.Play();
        
    }

    // stop chase music
    public void StopChaseMusic()
    {
        //Debug.Log("STOPPING CHASE MUSIC---------------");
        // fades chase music and then stops audio after 1 second
        chaseMusicAudioSource.DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(chaseMusicAudioSource.Stop);

        // sets audiosouce volume back to original
        
    }

    public void PlayCaughtMusic()
    {
        caughtAudioSource.volume = caughtVolume;
        //Debug.Log("Playing caught music!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        caughtAudioSource.Play();
    }

    // Easter egg for a friend who wanted his scream to be in the game
    private void PlayCreepyScream()
    {
        if (easterEggScreamAudioSource != null)
        {
            easterEggScreamAudioSource.Stop();
            easterEggScreamAudioSource.Play();
        }
    }

}
