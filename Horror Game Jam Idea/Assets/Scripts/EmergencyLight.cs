using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour
{

    private Light emergencyLight;
    private float lightIntensity = 2f;
    [SerializeField] private float maxLightItensity = 5f;
    [SerializeField] private float sirenLightTime = 2.5f;
    [SerializeField] private AnimationCurve lightCurve;
    //[SerializeField] private Renderer glassEmissionRend;
    private Material glassEmissionMaterial;
    private Color initialEmissionColor;

    // Start is called before the first frame update
    void Start()
    {
        emergencyLight = GetComponent<Light>();
        emergencyLight.DOIntensity(maxLightItensity, sirenLightTime).SetLoops(-1, LoopType.Yoyo).SetEase(lightCurve);
        //glassEmissionMaterial = glassEmissionRend.material;
        //initialEmissionColor = glassEmissionMaterial.GetColor("_EmissionColor");
        
        //glassEmissionMaterial.DOColor(Color.black, sirenLightTime).SetLoops(-1, LoopType.Yoyo).SetEase(lightCurve);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float speed = 2.3f;
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2.0f;
        Color col = Color.Lerp(initialEmissionColor, new Color(0.1f, 0f, 0, 1), t);

        // = glassEmissionMaterial.DOColor(Color.black, sirenLightTime);

        glassEmissionMaterial.SetColor("_EmissionColor", col);
        */
    }
}
