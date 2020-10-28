using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlashLight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Light flashLight;
    //private bool flashlightOn = false;

    private void Start()
    {
        flashLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLight.enabled == true)
            {
                flashLight.enabled = false;
            }
            else
            {
                flashLight.enabled = true;
            }
        }
        
    }
}
