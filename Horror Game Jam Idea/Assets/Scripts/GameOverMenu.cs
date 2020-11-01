using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quitting menu");
        Application.Quit();

    }
}
