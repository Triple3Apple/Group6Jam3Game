using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scareMeter = null;
    [SerializeField] private int maxScareCount = 4;
    [SerializeField] private TextMeshProUGUI keyCountUI = null;

    private static int scareCount = 0;

    private static int keysFound = 0;

    private void Awake()
    {
        // scareCount = 0;
        scareCount = maxScareCount;
    }

    private void Update()
    {
        if (scareCount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Make sure this is the correct scene
        }

        /*
        if(scareCount >= maxScareCount)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Make sure this is the correct scene
        }
        */

        scareMeter.text = "Scares Remaining: " + scareCount;

        if (keysFound < 3)
        {
            keyCountUI.text = "Keys Found: " + keysFound.ToString() + " / 3";
        }
        else
        {
            keyCountUI.text = "Keys Found: " + keysFound.ToString() + " / 3\nEscape the Facility!";
        }
        

    }

    public static void Scared()
    {
        scareCount -= 1;
    }

    public static void FoundKey()
    {
        keysFound += 1;
    }
}
