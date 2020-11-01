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

    [SerializeField] float fogIncreasePerSecond = 0.03f;

    [SerializeField] float fogLimit = 0.1f;

    private bool isIncreasingFog = false;
    private AudioManager audioManager;

    private bool justStartedGame = true;

    private void Awake()
    {
        //current = this;
        enemyManager = FindObjectOfType<EnemiesManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        // remove below
        //StartGameTimeManager();

        onTimeManagerStarted();
        StartCoroutine(RunUntilLightsOnTimerEnds());
    }

    private void Update()
    {
        if (isIncreasingFog)
        {
            RenderSettings.fogDensity += fogIncreasePerSecond * Time.deltaTime;

            // if fog reached limit then stop fog fade in
            if (RenderSettings.fogDensity >= fogLimit)
            {
                isIncreasingFog = false;
            }
        }
    }

    // this will/should be called by gamemanager when game officially starts
    public void StartGameTimeManager()
    {
        //Debug.Log("starting first coroutine");
        StartCoroutine(RunUntilLightsOnTimerEnds());
    }

    // runs until lightsOnTimer seconds has passed (LIGHTS ON)
    IEnumerator RunUntilLightsOnTimerEnds()
    {

        Debug.Log("LIGHTS ON");

        // makes it so the sound does not play when level is first started
        if (justStartedGame == false)
        {
            PlayPowerUpNoise();
        }
        else justStartedGame = false;
        
        RenderSettings.fog = false;
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
        Debug.Log("LIGHTS OFF");


        PlayPowerDownNoise(lightsOffTime);

        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0f;
        isIncreasingFog = true;
        //RenderSettings.fogDensity = 

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

    private void PlayPowerDownNoise(float lightsOffDuration)
    {
        audioManager.PlayPowerDownSound(lightsOffDuration);
    }

    private void PlayPowerUpNoise()
    {
        audioManager.PlayPowerUpSound();
    }
}
