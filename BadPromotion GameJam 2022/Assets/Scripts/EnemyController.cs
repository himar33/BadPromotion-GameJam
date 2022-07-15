using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject target;
    NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {        
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = target.transform.position;
        navAgent.stoppingDistance = 2.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.destination != target.transform.position)
        {
            navAgent.destination = target.transform.position;
        }

    }
}
