using SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy AI")]
    [SerializeField] private Sensor sightSensor;
    [SerializeField] private SteeringRig steering;

    [SerializeField] private Transform[] patrolPathWayPoints;

    // if false then the waypoint index will ping pong
    [SerializeField] bool wayPointsLooping = true;
    private bool ascendingIndex = true;

    [Header("State Durations")]
    // how much time spent investigating plays last position
    [SerializeField] float investigateStateDuration = 2.5f;
    [SerializeField] float pauseStateDuration = 2.5f;

    // how close (distance) the enemy has to get to waypoint to reach it
    [SerializeField] float waypointArrivalDistance = 3f;

    [Header("Enemy Mesh and Candle")]
    [SerializeField] GameObject enemyMesh;
    [SerializeField] GameObject candle;
    [SerializeField] GameObject scareCollider;

    [Header("Enemy Move Speed")]
    [SerializeField] private float defaultMoveSpeed = 5f;

    private int nextWaypointIndex;

    private EnemyAnimatorController enemyAnimController;

    private bool lightsAreOn = true;

    private bool isChasingPlayer = false;

    private EnemiesManager enemyManager;

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemiesManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //enemyManager = FindObjectOfType<EnemiesManager>();

        //sensor = GetComponent<FOVCollider>();
        steering.RotateTowardsTarget = false;
        enemyAnimController = GetComponent<EnemyAnimatorController>();
        //moveSpeed = steering.MoveSpeed;
        StartCoroutine(PatrolState());
    }

    IEnumerator PatrolState()
    {
        isChasingPlayer = false;
        enemyManager.NotChasingEnemy();

        nextWaypointIndex = getNearestWaypointIndex();

        Debug.Log("Current WayPoint Index");
        Debug.Log("PATROLING");

        enemyAnimController.SetAnimatorToWalk();

        Start:

        if (attackEnemyIfSpotted()) yield break;

        // make object head towards waypoint
        steering.DestinationTransform = patrolPathWayPoints[nextWaypointIndex];

        if ((transform.position - patrolPathWayPoints[nextWaypointIndex].position).magnitude < waypointArrivalDistance)
        {
            // We've arrived at our target waypoint. Select the next waypoint.
            
            if (wayPointsLooping)
            {
                // if the nextwaypoint is already on the last waypoint, then restart to zero
                nextWaypointIndex = (nextWaypointIndex + 1) == patrolPathWayPoints.Length ? 0 : nextWaypointIndex + 1;
                Debug.Log("nextWaypointIndex is now " + nextWaypointIndex);
            }
            else
            {
                // if the nextwaypoint is on the last waypoint (decrease index) or the first waypoint (increase index)
                if (ascendingIndex)
                {
                    nextWaypointIndex = (nextWaypointIndex + 1) == patrolPathWayPoints.Length ? nextWaypointIndex - 1 : nextWaypointIndex + 1;
                }
                else
                {
                    if ((nextWaypointIndex - 1) < 0)
                    {
                        nextWaypointIndex++;
                    }
                    else
                    {
                        nextWaypointIndex--;
                    }
                }
                //nextWaypointIndex = ascendingIndex ? nextWaypointIndex + 1 : nextWaypointIndex - 1;
                Debug.Log("nextWaypointIndex is now " + nextWaypointIndex);
            }

            // If this was the last waypoint in the sequence then pause for a moment before following
            // the waypoints in reverse.

            if (nextWaypointIndex == patrolPathWayPoints.Length - 1 || nextWaypointIndex == 0)
            {
                // if ping pong is true and the last
                if (!wayPointsLooping) ascendingIndex = !ascendingIndex;

                //StartCoroutine(PauseState()); yield break;
            }
        }

        yield return null;
        goto Start;
    }

    private bool attackEnemyIfSpotted()
    {
        if (sightSensor.GetDetectedByTag("Player").Count > 0 && lightsAreOn == false)
        {
            List<GameObject> detectedPlayerArray = sightSensor.GetDetectedByTag("Player");
            Debug.Log("Player detected");
            Debug.Log(detectedPlayerArray.Count);
            GameObject player = detectedPlayerArray[0];

            StartCoroutine(ChasePlayerState(player));

            //ChaseTarget(detectedPlayerArray[0]);

            return true;
        }

        return false;
    }

    IEnumerator ChasePlayerState(GameObject targetToChase)
    {
        isChasingPlayer = true;
        enemyManager.ChasingEnemy();

        Debug.Log("CHASING");
        //steering.DestinationTransform = null;

        steering.DestinationTransform = targetToChase.transform;    // new
        steering.FaceTowardsTransform = targetToChase.transform;

        // when enemy sees player, increase enemy movement speed by a bit
        steering.MoveSpeed = defaultMoveSpeed + 2f;


        enemyAnimController.SetAnimatorToRun();

        Start:

        // if no target found
        if (targetToChase == null)
        {
            steering.DestinationTransform = null;   // new

            StartCoroutine(PauseState());
            yield break;
        }

        // if enemy cant see player/target anymore (out of enemy's sight)
        if (!sightSensor.IsDetected(targetToChase))
        {
            steering.DestinationTransform = null;   // new

            steering.FaceTowardsTransform = null;
            //GunPivot.transform.localRotation = Quaternion.identity; // Return gun rotation back to resting position
            StartCoroutine(InvestigateLastLocationState(targetToChase.transform.position));
            yield break;
        }

        yield return null;
        goto Start;
    }

    IEnumerator InvestigateLastLocationState(Vector3 lastTargetPosition)
    {
        isChasingPlayer = false;
        enemyManager.NotChasingEnemy();

        //Debug.Log("INVESTIGATING");

        WaitUntilLightsOff:

        if (lightsAreOn == true)
        {
            //Debug.Log("investigating but WAITING");
            yield return null;
            goto WaitUntilLightsOff;
        }

        steering.DestinationTransform = null;
        steering.Destination = lastTargetPosition;
        float timer = 0f;

        steering.MoveSpeed = defaultMoveSpeed;

        enemyAnimController.SetAnimatorToIdle();

        Start:

        if (lightsAreOn == false)
        {
            if (attackEnemyIfSpotted()) yield break;

            timer += Time.deltaTime;
            if (timer > investigateStateDuration || !steering.IsSeeking)
            {
                StartCoroutine(PauseState()); yield break;
            }
        }

        yield return null;
        goto Start;
    }

    IEnumerator PauseState()
    {
        isChasingPlayer = false;
        enemyManager.NotChasingEnemy();

        Debug.Log("PAUSE-ING");
        steering.DestinationTransform = null;
        steering.Destination = transform.position;

        steering.MoveSpeed = defaultMoveSpeed;

        enemyAnimController.SetAnimatorToIdle();

        float timer = pauseStateDuration;
        while (timer > 0f)
        {
            if (attackEnemyIfSpotted()) yield break;

            timer -= Time.deltaTime;
            yield return null;
        }

        Start: 

        if (lightsAreOn == false)
        {
            // after pause state, go back to patrolling
            StartCoroutine(PatrolState()); yield break;
        }

        yield return null;
        goto Start;

    }

    private int getNearestWaypointIndex()
    {
        float nearestDist = 0f;
        int nearest = -1;
        for (int i = 0; i < patrolPathWayPoints.Length; i++)
        {
            var dist = (transform.position - patrolPathWayPoints[i].position).sqrMagnitude;
            if (dist < nearestDist || nearest == -1)
            {
                nearest = i;
                nearestDist = dist;
            }
        }
        return nearest;
    }

    // called by enemyManager (remember to add enemy to enemyManager list)
    public void DisableEnemy()
    {
        //Debug.Log("-----------------------Enemy mOVEsPEED = 0------------------------");
        isChasingPlayer = false;
        enemyManager.NotChasingEnemy();

        lightsAreOn = true;

        steering.MoveSpeed = 0f;
        enemyMesh.SetActive(false);
        scareCollider.SetActive(false);

        candle.SetActive(true);
        Debug.Log("Steering move speed = " + steering.MoveSpeed);
    }

    // called by enemyManager (remember to add enemy to enemyManager list)
    public void EnableEnemy()
    {
        //Debug.Log("-----------------------Enemy mOVEsPEED = NORMAL------------------------");

        lightsAreOn = false;

        steering.MoveSpeed = defaultMoveSpeed;
        enemyMesh.SetActive(true);
        scareCollider.SetActive(true);

        candle.SetActive(false);
    }

    // called by EnemiesManager to see if enemy is chasing the player
    public bool IsEnemyChasingPlayer() { return isChasingPlayer; }

    public void ScarePlayer()
    {
        enemyManager.ScarePlayerWithSound();
        DisableEnemy();
    }

}
