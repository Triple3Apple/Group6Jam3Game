using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{

    [SerializeField] private Transform clockHand;

    //[SerializeField] private float lightsOutTimer = 10f;
    //[SerializeField] private float lightsOnTimer = 60f;

    private float lightsOffTime;
    private float lightsOnTime;

    private float clockHandCurrentAngle = 0f;
    private float startAngle = -90f;

    private int loopCount = 0;
    private float initialXRot;

    // keep track of whether the clock cycle has ended
    private bool clockCycleFinished = false;


    // Start is called before the first frame update

    private void Awake()
    {
        SubscribeToTimeEvents();
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

    private void InitialiseClock()
    {
        clockHand.transform.rotation = Quaternion.Euler(-90f, clockHand.transform.rotation.y, clockHand.transform.rotation.z);
        Debug.Log("Initiualized clockhand");
        initialXRot = clockHand.rotation.x;
        InitializeLightsTime();

        //DoFourthClockCycleLightsOn();
    }

    private void InitializeLightsTime()
    {
        // find TimeManager object to set lightsOutTime and lightsOnTime
        TimeManager tm = FindObjectOfType<TimeManager>();
        lightsOnTime = tm.lightsOnTime;
        lightsOffTime = tm.lightsOffTime;

        Debug.Log("time for clock has been set up");
    }

    // subribing to time events
    private void SubscribeToTimeEvents()
    {
        TimeManager.onTimeManagerStarted += InitialiseClock;

        TimeManager.onLightsOnTimerStart += DoLightsOnActions;

        TimeManager.onLightsOffTimerStart += DoLightsOffActions;
    }

    // this will only run when the event onLightsOnTimerStart is recieved/heard
    private void DoLightsOnActions()
    {
        //clockCycleFinished = false; // set to false since clock cycle is being started again
        Debug.Log("ClockCycleOff coroutine called");
        DoFourthClockCycleLightsOn();
    }

    // this will only run when the event onLightsOffTimerStart is recieved/heard
    private void DoLightsOffActions()
    {
        //clockCycleFinished = false; // set to false since clock cycle is being started again
        Debug.Log("ClockCycleOff coroutine called");
        DoFourthClockCycleLightsOff();
    }

    // This method runs 4 times (each time it rotates the clock hand by 1/4th of the clock face)
    public void DoFourthClockCycleLightsOn()
    {
        if (loopCount >= 4)
        {
            Debug.Log("clockhand on has reached final rotation");

            //clockCycleFinished = true;

            loopCount = 0;
            //DoFourthClockCycleLightsOff();
            

            return;
        }

        Debug.Log("starting forth clock cycle lights on, loopCount = " + loopCount);
        loopCount++;
        clockHand.transform.DORotate(
        new Vector3(
            clockHand.transform.rotation.x + 90f,
            clockHand.transform.rotation.y,
            clockHand.transform.rotation.z),
        lightsOnTime / 4, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(DoFourthClockCycleLightsOn);

    }

    public void DoFourthClockCycleLightsOff()
    {
        if (loopCount >= 4)
        {
            Debug.Log("clockhand off has reached final rotation");

            //clockCycleFinished = true;
            loopCount = 0;
            //DoFourthClockCycleLightsOn();

            return;
        }

        Debug.Log("starting forth clock cycle lights off, loopCount = " + loopCount);
        loopCount++;
        clockHand.transform.DORotate(
        new Vector3(
            clockHand.transform.rotation.x + 90f,
            clockHand.transform.rotation.y,
            clockHand.transform.rotation.z),
        lightsOffTime / 4, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(DoFourthClockCycleLightsOff);

    }
}
