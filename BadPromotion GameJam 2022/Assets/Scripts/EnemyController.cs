using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject target;
    NavMeshAgent navAgent;

    [SerializeField] int life = 20;

    bool firstFrame = true;
    // Start is called before the first frame update
    void Start()
    {        
        navAgent = GetComponent<NavMeshAgent>();
        //navAgent.destination = target.transform.position;
        //navAgent.stoppingDistance = 2.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Die();
        }


        if (Vector3.Distance(target.transform.position, transform.position) > 5)
            return;

        if (firstFrame)
        {
            navAgent.destination = target.transform.position;
            navAgent.stoppingDistance = 2.1f;
        }

        if (navAgent.destination != target.transform.position)
        {
            navAgent.destination = target.transform.position;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Token")
        {
            life -= 5;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
