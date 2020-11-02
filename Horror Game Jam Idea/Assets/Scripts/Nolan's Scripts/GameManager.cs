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

    private static int scareCount = 0;

    private void Awake()
    {
        scareCount = 0;
    }

    private void Update()
    {
        if(scareCount >= maxScareCount)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Make sure this is the correct scene
        }

        scareMeter.text = "Scare Meter: " + scareCount.ToString() + " / " + maxScareCount;
    }

    public static void Scared()
    {
        scareCount += 1;
    }
}
