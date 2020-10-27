using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LightsManager : MonoBehaviour
{
    //[SerializeField] private EmergencyLight[] emergencyLights;
    //private GameObject emergencyLightsContainer;

    [SerializeField] private List<EmergencyLight> emergencyLights;
    //[SerializeField] private List<NormalLight> normalLights;

    private LightmapManagerTest lightmapManager;


    private void Awake()
    {
        SubscribeToTimeEvents();
        LocateEmergencyLights();
        lightmapManager = FindObjectOfType<LightmapManagerTest>();
    }

    private void LocateEmergencyLights()
    {
        /*
        foreach (Transform child in emergencyLightsContainer.transform)
        {
            Debug.Log(child.gameObject.GetComponent<EmergencyLight>() != null);
            EmergencyLight el = child.gameObject.GetComponent<EmergencyLight>();
            emergencyLights.Add(el);
        }
        */

        //emergencyLights = FindObjectsOfType<EmergencyLight>();
    }

    private void SubscribeToTimeEvents()
    {
        TimeManager.onLightsOnTimerStart += DoLightsOnActions;

        TimeManager.onLightsOffTimerStart += DoLightsOffActions;
    }

    private void DoLightsOnActions()
    {
        lightmapManager.ToggleLightsOn();

        foreach (EmergencyLight emerLight in emergencyLights)
        {
            emerLight.StopLights();
        }

        // new comment out!!!!!!!!!!!!
        /*
        foreach (NormalLight normLight in normalLights)
        {
            normLight.ActivateLights();
        }*/


    }

    private void DoLightsOffActions()
    {
        lightmapManager.ToggleLightsOff();

        Debug.Log("DoLightsOffActions() was called");

        // new comment out!!!!!!!!!!!!
        /*
        foreach (NormalLight normLight in normalLights)
        {
            normLight.StopLights();
        }
        */

        foreach (EmergencyLight emerLight in emergencyLights)
        {
            emerLight.ActivateEmergencyLights();
        }
    }
}
