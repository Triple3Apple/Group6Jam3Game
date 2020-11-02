using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void GoMainMenu()
    {
        Debug.Log("Going to main menu");
        SceneManager.LoadScene(0);

    }

    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
