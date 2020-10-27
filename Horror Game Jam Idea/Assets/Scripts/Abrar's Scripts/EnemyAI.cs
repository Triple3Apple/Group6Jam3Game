using SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Sensor sightSensor;
    [SerializeField] private SteeringRig steering;

    [SerializeField] private Transform[] patrolPathWayPoints;

    // how much time spent investigating plays last position
    [SerializeField] float investigateStateDuration = 2.5f;
    [SerializeField] float pauseStateDuration = 2.5f;

    // how close (distance) the enemy has to get to waypoint to reach it
    [SerializeField] float waypointArrivalDistance = 3f;

    // if false then the waypoint index will ping pong
    [SerializeField] bool wayPointsLooping = true;
    private bool ascendingIndex = true;

    [SerializeField] GameObject enemyMesh;

    [SerializeField] private float defaultMoveSpeed = 5f;

    private int nextWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        //sensor = GetComponent<FOVCollider>();
        steering.RotateTowardsTarget = false;
        //moveSpeed = steering.MoveSpeed;
        StartCoroutine(PatrolState());
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (sightSensor.GetDetectedByTag("Player").Count > 0)
        {
            List<GameObject> detectedPlayerArray = sightSensor.GetDetectedByTag("Player");
            Debug.Log("Player detected");
            Debug.Log(detectedPlayerArray.Count);
            ChaseTarget(detectedPlayerArray[0]);
        }
        else
        {

            if (Vector3.Distance(patrolPathWayPoints[0].position, transform.position) > 5)
            {
                GoToPatrolPoint(patrolPathWayPoints[0]);
                Debug.Log("Going to patrol point");
            }
            else
            {
                Idle();
                Debug.Log("Player NOT detected");
            }
            
        }
    }
    */

    IEnumerator PatrolState()
    {
        nextWaypointIndex = getNearestWaypointIndex();

        Debug.Log("Current WayPoint Index");

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
        if (sightSensor.GetDetectedByTag("Player").Count > 0)
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
        //steering.DestinationTransform = null;

        steering.DestinationTransform = targetToChase.transform;    // new
        steering.FaceTowardsTransform = targetToChase.transform;

        // when enemy sees player, increase enemy movement speed by a bit
        steering.MoveSpeed = defaultMoveSpeed + 1f;

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

        // Roate the gun in hand to face the enemy, reload if empty, otherwise fire the gun.
        //GunPivot.transform.LookAt(new Vector3(ToAttack.transform.position.x, GunPivot.transform.position.y, ToAttack.transform.position.z));
        //if (gun.IsEmptyClip) gun.Reload();
        //else gun.Fire();

        yield return null;
        goto Start;

    }

    IEnumerator InvestigateLastLocationState(Vector3 lastTargetPosition)
    {

        steering.DestinationTransform = null;
        steering.Destination = lastTargetPosition;
        float timer = 0f;

        steering.MoveSpeed = defaultMoveSpeed;

        Start:

        if (attackEnemyIfSpotted()) yield break;

        timer += Time.deltaTime;
        if (timer > investigateStateDuration || !steering.IsSeeking)
        {
            StartCoroutine(PauseState()); yield break;
        }

        yield return null;
        goto Start;
    }

    IEnumerator PauseState()
    {
        steering.DestinationTransform = null;
        steering.Destination = transform.position;

        steering.MoveSpeed = defaultMoveSpeed;

        float timer = pauseStateDuration;
        while (timer > 0f)
        {
            if (attackEnemyIfSpotted()) yield break;

            timer -= Time.deltaTime;
            yield return null;
        }

        // after pause state, go back to patrolling
        StartCoroutine(PatrolState()); yield break;
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

    public void DisableEnemy()
    {
        steering.MoveSpeed = 0f;
        enemyMesh.SetActive(false);
    }

    public void EnableEnemy()
    {
        Debug.Log("--------MOVE speed: " + defaultMoveSpeed);
        steering.MoveSpeed = defaultMoveSpeed;
        enemyMesh.SetActive(true);
    }





    private void ChaseTarget(GameObject target)
    {
        var speed = 4f;
        transform.LookAt(target.transform);
        //transform.position += transform.forward * speed * Time.deltaTime;
        steering.DestinationTransform = target.transform;
    }

    private void Idle()
    {
        steering.DestinationTransform = null;
        // do nothing
    }

    private void GoToPatrolPoint(Transform target)
    {
        var speed = 4f;
        transform.LookAt(target.transform);
        //transform.position += transform.forward * speed * Time.deltaTime;
        steering.DestinationTransform = target.transform;
    }
}
