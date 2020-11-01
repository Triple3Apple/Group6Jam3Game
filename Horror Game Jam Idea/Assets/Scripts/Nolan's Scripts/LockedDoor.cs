using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject lockedPrompt = null;

    private void OnValidate()
    {
        if(lockedPrompt == null)
        {
            lockedPrompt = GameObject.Find("LockedDoorPrompt");
        }
    }

    public void OnEndHover()
    {
        return;
    }

    public void OnInteract()
    {
        //Debug.Log("This door won't budge.");
        lockedPrompt.SetActive(true);
        StartCoroutine("UITimeout");
    }

    public void OnStartHover()
    {
        return;
    }

    private IEnumerator UITimeout()
    {
        yield return new WaitForSeconds(1f);
        lockedPrompt.SetActive(false);
    }
}
