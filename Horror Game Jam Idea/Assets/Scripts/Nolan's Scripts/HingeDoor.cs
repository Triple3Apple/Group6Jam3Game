﻿using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HingeDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isKeyed = false;
    [SerializeField] private Item.ItemType keyRequired = Item.ItemType.BronzeKey;
    [SerializeField] private GameObject keyPrompt = null;

    [SerializeField] private AudioClip doorOpenSFX = null;
    [SerializeField] private AudioClip doorCloseSFX = null;

    [SerializeField] private Transform pivotPoint = null;
    [SerializeField] private float duration = 2.4f;
    [SerializeField] private float openDegree = 0f;
    [SerializeField] private float closedDegree = 90f;

    private AudioSource audioSource;
    private bool isOpen = false;
    private Vector3 openRot;
    private Vector3 closedRot;

    // added by abrar for unlock sound
    [Header("Optional")]
    [SerializeField] private AudioSource unlockAudioSource;
    private bool unlockSoundPlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        openRot = new Vector3(0, openDegree, 0);
        closedRot = new Vector3(0, closedDegree, 0);
    }

    private void OpenDoor()
    {
        PlayUnlockSound();
        audioSource.PlayOneShot(doorOpenSFX, .2f);
        pivotPoint.DORotate(openRot, duration, RotateMode.Fast);
    }

    private void CloseDoor()
    {
        audioSource.PlayOneShot(doorCloseSFX, .2f);
        pivotPoint.DORotate(closedRot, duration, RotateMode.Fast);
    }

    private IEnumerator UITimeout()
    {
        yield return new WaitForSeconds(1f);
        keyPrompt.SetActive(false);
    }

    public void OnStartHover()
    {
        //Debug.Log($"{gameObject.name} is ready to be opened.");
    }

    public void OnInteract()
    {
        if (isKeyed)
        {
            if (!Player.SearchForItem(new Item { itemType = keyRequired }))
            {
                keyPrompt.SetActive(true);
                keyPrompt.GetComponent<Text>().text = keyRequired.ToString() + " is required.";
                StartCoroutine("UITimeout");
                return;
            }
        }

        if (!isOpen)
        {
            audioSource.Stop();
            OpenDoor();
            isOpen = true;
        }
        else
        {
            audioSource.Stop();
            CloseDoor();
            isOpen = false;
        }
    }

    public void OnEndHover()
    {
        //Debug.Log($"{gameObject.name} cannot be opened anymore.");
    }

    private void PlayUnlockSound()
    {
        if (unlockAudioSource == null)  return;

        if (isKeyed == true && unlockSoundPlayed == false)
        {
            unlockSoundPlayed = true;
            unlockAudioSource.Play();
        }
    }
}
