using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    // Start is called before the first frame update
    void Awake()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }

    public void EnableAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Debug.Log("++++++++enabled enemy " + enemy.name);

            //enemy.SetActive(true);

            enemy.GetComponent<EnemyAI>().EnableEnemy();

            
        }
    }

    public void DisableAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Debug.Log("++++++++disabled enemy " + enemy.name);

            enemy.GetComponent<EnemyAI>().DisableEnemy();

            //enemy.SetActive(false);
        }
    }

}
