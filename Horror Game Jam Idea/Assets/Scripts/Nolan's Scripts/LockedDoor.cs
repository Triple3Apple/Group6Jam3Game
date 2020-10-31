using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip lockSFX = null;

    private AudioSource audioSource;

    private void OnValidate()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnEndHover()
    {
        return;
    }

    public void OnInteract()
    {
        audioSource.PlayOneShot(lockSFX);
        Debug.Log("This door won't budge.");
    }

    public void OnStartHover()
    {
        return;
    }
}
