using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private float range = 5f;

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
                if(_interactable == currentTarget)
                {
                    return;
                }
                else if (currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = _interactable;
                    currentTarget.OnStartHover();
                    return;
                }
                else
                {
                    currentTarget = _interactable;
                    currentTarget.OnStartHover();
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
    }
}
