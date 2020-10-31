using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private GameObject interactText = null;

    private IInteractable currentTarget;
    private Camera playerCam;

    private void Awake()
    {
        playerCam = GetComponent<Camera>();
    }

    private void Update()
    {
        RaycastForInteraction();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentTarget != null)
            {
                currentTarget.OnInteract();
            }
        }
    }

    private void RaycastForInteraction()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, range))
        {
            IInteractable _interactable = hit.collider.GetComponent<IInteractable>();

            if(_interactable != null)
            {
                if (hit.transform.gameObject.CompareTag("Interactable"))
                {
                    interactText.SetActive(true);
                }

                if (_interactable == currentTarget)
                {
                    return;
                }
                else if (currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = _interactable;
                    currentTarget.OnStartHover();

                    //if (hit.transform.gameObject.CompareTag("Interactable"))
                    //{
                    //    interactText.SetActive(true);
                    //}

                    return;
                }
                else
                {
                    currentTarget = _interactable;
                    currentTarget.OnStartHover();

                    //if (hit.transform.gameObject.CompareTag("Interactable"))
                    //{
                    //    interactText.SetActive(true);
                    //}

                    return;
                }
            }
            else
            {
                if(currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }
        else
        {
            if(currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }

        interactText.SetActive(false);
    }
}
