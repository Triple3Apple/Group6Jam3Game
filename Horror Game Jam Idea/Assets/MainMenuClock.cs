using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuClock : MonoBehaviour
{
    [SerializeField] private Transform clockHand;

    //[SerializeField] private float lightsOutTimer = 10f;
    //[SerializeField] private float lightsOnTimer = 60f;

    private float lightsOffTime = 15f;
    private float lightsOnTime = 15f;

    private float clockHandCurrentAngle = 0f;
    private float startAngle = -90f;

    //private int loopCount = 0;

    private int lightOnLoopCount = 0;
    private int lightOffLoopCount = 0;

    private float initialXRot;

    // keep track of whether the clock cycle has ended
    private bool clockCycleFinished = false;

    //[SerializeField] private AnimationCurve clockSoundCurve;

    private AudioSource clockSoundSource;

    //private AudioManager audioManager;


    // Start is called before the first frame update

    private void Awake()
    {
        //SubscribeToTimeEvents();

        //clockSoundSource = GetComponent<AudioSource>();

        //audioManager = FindObjectOfType<AudioManager>();
    }
    /*
    void Start()
    {
        clockHand.transform.rotation = Quaternion.Euler(-90f, clockHand.transform.rotation.y, clockHand.transform.rotation.z);
        Debug.Log("Initiualized clockhand");
        initialXRot = clockHand.rotation.x;
        InitializeLightsTime();

        // DoFourthClockCycleLightsOn();
    }
    */

    private void Start()
    {
        InitialiseClock();

        DoFourthClockCycleLightsOn();
    }

    private void InitialiseClock()
    {
        clockHand.transform.localRotation = Quaternion.Euler(-90f, clockHand.transform.localRotation.y, clockHand.transform.localRotation.z);
        //Debug.Log("Initiualized clockhand");
        initialXRot = clockHand.localRotation.x;
        InitializeLightsTime();

        //DoFourthClockCycleLightsOn();
    }

    private void InitializeLightsTime()
    {
        // find TimeManager object to set lightsOutTime and lightsOnTime
        //TimeManager tm = FindObjectOfType<TimeManager>();
        //lightsOnTime = tm.lightsOnTime;
        //Debug.Log("lights on time: " + lightsOnTime);
        //lightsOffTime = tm.lightsOffTime;
        //Debug.Log("lights off time: " + lightsOffTime);

        //Debug.Log("time for clock has been set up");
    }

    // subribing to time events

    // this will only run when the event onLightsOnTimerStart is recieved/heard
    private void DoLightsOnActions()
    {
        //clockCycleFinished = false; // set to false since clock cycle is being started again
        //Debug.Log("ClockCycleOff coroutine called");
        DoFourthClockCycleLightsOn();
    }

    // this will only run when the event onLightsOffTimerStart is recieved/heard
    private void DoLightsOffActions()
    {
        //clockCycleFinished = false; // set to false since clock cycle is being started again
        //Debug.Log("ClockCycleOff coroutine called");
        DoFourthClockCycleLightsOff();
    }

    // This method runs 4 times (each time it rotates the clock hand by 1/4th of the clock face)
    public void DoFourthClockCycleLightsOn()
    {
        if (lightOnLoopCount >= 4)
        {
            //Debug.Log("clockhand on has reached final rotation");

            //clockCycleFinished = true;

            lightOnLoopCount = 0;
            DoFourthClockCycleLightsOff();


            return;
        }

        if (lightOnLoopCount == 3)
        {
            IncreaseClockAudio();
        }

        //Debug.Log("starting forth clock cycle lights on, loopCount = " + loopCount);
        lightOnLoopCount++;
        //Debug.Log("+++++++++++++++Lights ON Time/4 = " + lightsOnTime / 4 + " Loop Count = " + lightOnLoopCount);
        clockHand.transform.DORotate(
        new Vector3(
            clockHand.transform.localRotation.x + 90f,
            clockHand.transform.localRotation.y,
            clockHand.transform.localRotation.z),
        lightsOnTime / 4, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(DoFourthClockCycleLightsOn);
        /*
        clockHand.transform.DORotate(
        new Vector3(
            clockHand.transform.rotation.x + 90f,
            clockHand.transform.rotation.y,
            clockHand.transform.rotation.z),
        lightsOnTime / 4, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(DoFourthClockCycleLightsOn);
        */

    }

    public void DoFourthClockCycleLightsOff()
    {
        if (lightOffLoopCount >= 4)
        {
            //Debug.Log("clockhand off has reached final rotation");

            //clockCycleFinished = true;
            lightOffLoopCount = 0;
            DoFourthClockCycleLightsOn();

            return;
        }

        if (lightOffLoopCount == 3)
        {
            IncreaseClockAudio();
        }

        //Debug.Log("starting forth clock cycle lights off, loopCount = " + loopCount);
        lightOffLoopCount++;
        //Debug.Log("+++++++++++++++Lights OFF Time/4 = " + lightsOffTime / 4 + " Loop Count = " + lightOffLoopCount);
        clockHand.transform.DORotate(
        new Vector3(
            clockHand.transform.localRotation.x + 90f,
            clockHand.transform.localRotation.y,
            clockHand.transform.localRotation.z),
        lightsOffTime / 4, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(DoFourthClockCycleLightsOff);
        /*
        clockHand.transform.DORotate(
        new Vector3(
            clockHand.transform.rotation.x + 90f,
            clockHand.transform.rotation.y,
            clockHand.transform.rotation.z),
        lightsOffTime / 4, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(DoFourthClockCycleLightsOff);
        */

    }

    private void IncreaseClockAudio()
    {
        //Debug.Log("Increasing clock Audio");
        //float initClockVolume = clockSoundSource.volume;
        //clockSoundSource.DOFade(0.5f, lightsOnTime / 4f).SetEase(clockSoundCurve).OnComplete(() => ResetAudioVolume(initClockVolume));
    }

    //private void ResetAudioVolume(float vol) { clockSoundSource.volume = vol; }
}
