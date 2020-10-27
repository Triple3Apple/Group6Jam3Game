using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip doorOpenSFX = null;
    [SerializeField] private AudioClip doorCloseSFX = null;
    [SerializeField] private Transform pivotPoint = null;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float openDegree = 0f;
    [SerializeField] private float closedDegree = 90f;

    private AudioSource audioSource;
    private bool isOpen = false;
    private Quaternion openRot;
    private Quaternion closedRot;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        openRot = Quaternion.Euler(0, openDegree, 0);
        closedRot = Quaternion.Euler(0, closedDegree, 0);
    }

    private void OpenDoor()
    {
        audioSource.PlayOneShot(doorOpenSFX, .1f);
        pivotPoint.DORotateQuaternion(openRot, speed);
    }

    private void CloseDoor()
    {
        audioSource.PlayOneShot(doorCloseSFX, .1f);
        pivotPoint.DORotateQuaternion(closedRot, speed);
    }

    public void OnStartHover()
    {
        Debug.Log($"{gameObject.name} is ready to be opened.");
    }

    public void OnInteract()
    {
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
        Debug.Log($"{gameObject.name} cannot be opened anymore.");
    }
}
