using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundTracks;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ChooseTrack();
    }

    public void ChooseTrack()
    {
        int _rand = Random.Range(0, soundTracks.Length);

        audioSource.PlayOneShot(soundTracks[_rand], 0.15f);
    }
}
