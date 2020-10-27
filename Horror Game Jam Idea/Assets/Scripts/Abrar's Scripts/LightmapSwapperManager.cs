using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightmapSwapperManager : MonoBehaviour
{

    private AlternativeLightsManager altLightManager;
    // Start is called before the first frame update
    void Start()
    {
        altLightManager = FindObjectOfType<AlternativeLightsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("detected space, called altLightManager");
            altLightManager.SwapAllProbes(0);
        }
    }
}
