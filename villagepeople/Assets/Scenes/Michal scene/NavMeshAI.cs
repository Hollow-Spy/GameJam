using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    public List<Transform> Waypoints;
    private int waypointIndex = 0;

    public Transform player;

    public static bool isPatrolling = true;

    [SerializeField] private Transform directionPosition;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        SetTarget(Waypoints[waypointIndex]);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Update()
    {

        agent.destination = directionPosition.position;

        if (isPatrolling)
        {
            Patrol();
            agent.stoppingDistance = 0f;
        }
        else
        {
            SetTarget(player);
            agent.stoppingDistance = 3f;
        }
        

        
          

        // if (target = null) - go back to patroling
    }


    public void Patrol()
    {
        SetTarget(Waypoints[waypointIndex]);

        if (Vector3.Distance(transform.position, directionPosition.position) <= 0.5f)
        {
            if (waypointIndex >= Waypoints.Count - 1)
            {
                waypointIndex = 0;
                SetTarget(Waypoints[waypointIndex]);

                Debug.Log("Index: " + waypointIndex);

                return;
            }
            waypointIndex++;
            SetTarget(Waypoints[waypointIndex]);

        }
    }




    public void SetTarget(Transform target)
    {
        if (target != null)
        {
            directionPosition = target;
            
        }
        else
        {
            directionPosition = Waypoints[waypointIndex];
        }

    }

    public void AddLastKnownPos(Transform player)
    {
        Waypoints.Add(player);
        SetTarget(Waypoints[Waypoints.Count]);
    }

    public void LookAround()
    {
        float speed = 1f;
        transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, 90f, 0f, 0f), speed * Time.deltaTime);
    }


    public Transform GetLastPos()
    {
        return Waypoints[Waypoints.Count];
    }

}
