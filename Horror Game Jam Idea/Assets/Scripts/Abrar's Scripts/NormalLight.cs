using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLight : MonoBehaviour
{
    [SerializeField] Light normalLight;

    public void StopLights()
    {
        
        normalLight.enabled = false;
        
    }

    public void ActivateLights()
    {
        
        normalLight.enabled = true;
        
    }
}
