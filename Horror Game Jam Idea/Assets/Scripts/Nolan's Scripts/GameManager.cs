using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scareMeter = null;

    private static int scareCount = 0;

    private void Update()
    {
        if(scareCount >= 4)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Make sure this is the correct scene
        }

        scareMeter.text = "Scare Meter: " + scareCount.ToString() + " / 4" ;
    }
}
