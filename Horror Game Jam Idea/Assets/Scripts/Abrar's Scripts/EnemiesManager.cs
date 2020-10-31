using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    // Start is called before the first frame update

    // will be used to determine when to play chase music
    public static bool IsPlayingChaseMusic = false;

    private AudioManager audioManager;

    void Awake()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void EnableAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Debug.Log("++++++++enabled enemy " + enemy.name);

            enemy.GetComponent<EnemyAI>().EnableEnemy();
        }
    }

    public void DisableAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Debug.Log("++++++++disabled enemy " + enemy.name);

            enemy.GetComponent<EnemyAI>().DisableEnemy();
        }

        // force stop chase music
        audioManager.StopChaseMusic();
        IsPlayingChaseMusic = false;
    }

    public void ChasingEnemy()
    {
        // check if already playing chase music
        if (IsPlayingChaseMusic == false)
        {
            Debug.Log("playing CHASE MUSIC");
            IsPlayingChaseMusic = true;
            audioManager.PlayChaseMusic();
        }
    }

    public void NotChasingEnemy()
    {
        if (AreEnemiesChasing() == false)
        {
            Debug.Log("stopping CHASE MUSIC");
            // if there are no enemies that are chasing the player then stop the chase music
            audioManager.StopChaseMusic();
            IsPlayingChaseMusic = false;
        }
    }

    // checks if any enemies are chasing the player
    private bool AreEnemiesChasing()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyAI>().IsEnemyChasingPlayer() == true)
            {
                return true;
            }
        }

        return false;
    }

    public void ScarePlayerWithSound()
    {
        audioManager.PlayCaughtMusic();
    }

}
