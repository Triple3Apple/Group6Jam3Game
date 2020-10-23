using SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Sensor sensor;
    [SerializeField] private SteeringRig Steering;

    [SerializeField] private Transform[] patrolPath;

    // Start is called before the first frame update
    void Start()
    {
        //sensor = GetComponent<FOVCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sensor.GetDetectedByTag("Player").Count > 0)
        {
            List<GameObject> detectedPlayerArray = sensor.GetDetectedByTag("Player");
            Debug.Log("Player detected");
            Debug.Log(detectedPlayerArray.Count);
            ChaseTarget(detectedPlayerArray[0]);
        }
        else
        {

            if (Vector3.Distance(patrolPath[0].position, transform.position) > 5)
            {
                GoToPatrolPoint(patrolPath[0]);
                Debug.Log("Going to patrol point");
            }
            else
            {
                Idle();
                Debug.Log("Player NOT detected");
            }
            
        }
    }

    private void ChaseTarget(GameObject target)
    {
        var speed = 4f;
        transform.LookAt(target.transform);
        //transform.position += transform.forward * speed * Time.deltaTime;
        Steering.DestinationTransform = target.transform;
    }

    private void Idle()
    {
        Steering.DestinationTransform = null;
        // do nothing
    }

    private void GoToPatrolPoint(Transform target)
    {
        var speed = 4f;
        transform.LookAt(target.transform);
        //transform.position += transform.forward * speed * Time.deltaTime;
        Steering.DestinationTransform = target.transform;
    }
}
