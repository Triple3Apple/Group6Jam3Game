using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public static TimeManager current;

    //public event Action onLightsOnTimerStart;
    //public event Action onLightsOffTimerStart;

    public static event Action onTimeManagerStarted = delegate { };

    public static event Action onLightsOnTimerStart = delegate { };
    public static event Action onLightsOffTimerStart = delegate { };

    public int lightsOnTime = 20;
    public int lightsOffTime = 10;

    private EnemiesManager enemyManager;

    private void Awake()
    {
        //current = this;
        enemyManager = FindObjectOfType<EnemiesManager>();
    }

    void Start()
    {
        // remove below
        //StartGameTimeManager();

        onTimeManagerStarted();
        StartCoroutine(RunUntilLightsOnTimerEnds());
    }

    // this will/should be called by gamemanager when game officially starts
    public void StartGameTimeManager()
    {
        Debug.Log("starting first coroutine");
        StartCoroutine(RunUntilLightsOnTimerEnds());
    }

    // runs until lightsOnTimer seconds has passed (LIGHTS ON)
    IEnumerator RunUntilLightsOnTimerEnds()
    {
        //enable all enemies
        enemyManager.DisableAllEnemies();

        //NotifyThatLightsAreOn();
        //Debug.Log("Notifying that lights are now on");
        onLightsOnTimerStart();


        yield return new WaitForSeconds(lightsOnTime);

        StartCoroutine(RunUntilLightsOffTimerEnds());
    }

    //  (LIGHTS OFF)
    IEnumerator RunUntilLightsOffTimerEnds()
    {
        //enable all enemies
        enemyManager.EnableAllEnemies();

        //NotifyThatLightsAreOff();
        //Debug.Log("Notifying that lights are now off");
        onLightsOffTimerStart();

        yield return new WaitForSeconds(lightsOffTime);

        StartCoroutine(RunUntilLightsOnTimerEnds());
    }

    private void NotifyThatLightsAreOn()
    {
        // fire event
        //Debug.Log("Notifying that lights are now on");
        onLightsOnTimerStart();
    }

    private void NotifyThatLightsAreOff()
    {
        // fire event
        //debug.Log("Notifying that lights are now off");
        onLightsOffTimerStart();
    }
}
