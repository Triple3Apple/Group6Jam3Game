using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEmergencyLight : MonoBehaviour
{
    [SerializeField] private Light emergencyLight;
    private float lightIntensity = 2f;
    [SerializeField] private float maxLightItensity = 5f;
    [SerializeField] private float sirenLightTime = 2.5f;
    [SerializeField] private AnimationCurve lightCurve;
    [SerializeField] private Renderer glassEmissionRend;
    private Material glassEmissionMaterial;
    private Color initialEmissionColor;
    [SerializeField] private float initalLightItensity = 0.01f;

    Tween currentTween;

    [SerializeField] Material normalGlassMaterial;
    [SerializeField] Material litGlassMaterial;

    //private DOTween currentTween;

    // Start is called before the first frame update
    void Awake()
    {
        initalLightItensity = emergencyLight.intensity;
        //InitializeLightVariables();
    }

    private void Start()
    {
        ActivateEmergencyLights();
    }

    private void InitializeLightVariables()
    {
        //glassEmissionMaterial = glassEmissionRend.material;
        //initialEmissionColor = glassEmissionMaterial.GetColor("_EmissionColor");
        //glassEmissionMaterial.SetColor("_EmissionColor", Color.black);
    }

    public void StopLights()
    {
        //InitializeLightVariables();
        // set emergency light emission color to almost black
        //Color col = new Color(0.1f, 0f, 0f, 1f);
        //glassEmissionMaterial.SetColor("_EmissionColor", col);
        glassEmissionRend.material = normalGlassMaterial;
        //Debug.Log("Stop emergency Light called---------------------------");

        //emergencyLight.DOIntensity(initalLightItensity, 0.5f);
        currentTween.Kill();
        emergencyLight.enabled = false;

    }

    public void ActivateEmergencyLights()
    {
        // set emergency light emission color to red
        //glassEmissionMaterial.SetColor("_EmissionColor", initialEmissionColor);

        //Debug.Log("activate emergency light called---------------------------");
        glassEmissionRend.material = litGlassMaterial;
        emergencyLight.enabled = true;
        emergencyLight.intensity = initalLightItensity;
        currentTween = emergencyLight.DOIntensity(maxLightItensity, sirenLightTime).SetLoops(-1, LoopType.Yoyo).SetEase(lightCurve);
    }
}
