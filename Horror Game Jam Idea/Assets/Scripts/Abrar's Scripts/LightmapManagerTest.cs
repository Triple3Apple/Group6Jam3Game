using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightmapManagerTest : MonoBehaviour
{
    // helpfl vids : https://www.youtube.com/watch?v=rhhRFtHs1Lg&ab_channel=BigDipper (used as reference for code)
    //             : https://www.youtube.com/watch?v=BRapbR6vPII&ab_channel=BirdmaskStudio (used as reference on how to make multiple lightmaps)



    // MUST CHANGE THESE TEXTURES TO MAKE LIGHTMAP UPDATE CORRECTLY (when adding/baking new maps update these)
    [Header("bright lightmap textures")]
    [SerializeField] private Texture2D[] brightDirTextures;
    [SerializeField] private Texture2D[] brightLightTextures;

    [Header("dark lightmap textures")]
    [SerializeField] private Texture2D[] darkDirTextures;
    [SerializeField] private Texture2D[] darkLightTextures;

    private LightmapData[] lightMapsBright = new LightmapData[1];
    private LightmapData[] lightMapsDark = new LightmapData[1];

    private bool isDark = false;


    // Start is called before the first frame update
    void Start()
    {
        int lightmapTexturesCount = darkDirTextures.Length;
        //lightMapsBright = new LightmapData[lightmapTexturesCount];
        Debug.Log("brightDirTextures size: " + brightDirTextures.Length);
        Debug.Log("brightDirTextures size: " + brightDirTextures[0]);

        for (int i = 0; i < brightDirTextures.Length; i++)
        {
            lightMapsBright[i] = new LightmapData(); // very important
            lightMapsBright[i].lightmapDir = brightDirTextures[i];
        }

        Debug.Log("brightLightTextures size: " + brightLightTextures.Length);
        for (int i = 0; i < brightLightTextures.Length; i++)
        {
            lightMapsBright[i].lightmapColor = brightLightTextures[i];
            Debug.Log("brightLightTextures");
        }

        //-------------
        //lightMapsDark = new LightmapData[lightmapTexturesCount];

        for (int i = 0; i < darkDirTextures.Length; i++)
        {
            lightMapsDark[i] = new LightmapData();  // very important
            lightMapsDark[i].lightmapDir = darkDirTextures[i];
        }

        for (int i = 0; i < darkLightTextures.Length; i++)
        {
            lightMapsDark[i].lightmapColor = darkLightTextures[i];
        }

        // initial scene setup
        LightmapSettings.lightmaps = lightMapsBright;
    }

    // Update is called once per frame
    void Update()
    {
        // remove BELOW, was testing only 
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");

            if (isDark == true)
            {
                isDark = false;
                LightmapSettings.lightmaps = lightMapsBright;
            }
            else
            {
                isDark = true;
                LightmapSettings.lightmaps = lightMapsDark;
            }
        }
        */
    }

    public void ToggleLightsOn()
    {
        if (isDark == true)
        {
            isDark = false;
            LightmapSettings.lightmaps = lightMapsBright;
        }
        
    }

    public void ToggleLightsOff()
    {
        if (isDark == false)
        {
            isDark = true;
            LightmapSettings.lightmaps = lightMapsDark;
        }
    }
}
