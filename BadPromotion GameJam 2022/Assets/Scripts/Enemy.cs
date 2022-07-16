using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum TypeEnemy {walker,flying };
    public int live;
    public float speed;

    [SerializeField] TypeEnemy typeEnemy;

    Transform targetWaypoint;
    public NavMeshAgent agent;
    public GameObject[] waypoints;
    int patrolWP = 4;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //agent.destination = wayPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f) PatrolPattern();
    }

    void PatrolPattern()
    {
        patrolWP = (patrolWP + 1) % waypoints.Length;
        Seek(waypoints[patrolWP].transform.position);
    }

    void Seek(Vector3 targetPosition)
    {
        agent.destination = targetPosition;
    }
}
