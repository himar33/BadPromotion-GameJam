using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] GameObject target;
    NavMeshAgent navAgent;
    public int alertArea;

    enum TypeEnemy { walker, flying };
    [SerializeField] TypeEnemy typeEnemy;


    [SerializeField] int life = 2;
    [SerializeField] int damage = 5;

    [SerializeField] float attackCooldown = 1.5f;
    float lasAttackCount = 0f;

    bool firstFrame = true;

    public GameObject[] waypoints;
    int patrolWP = 4;


    // Start is called before the first frame update
    void Start()
    {        
        navAgent = GetComponent<NavMeshAgent>();
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0) Die();
        
        float distance = navAgent.remainingDistance;

        //Patrol
        if ((!navAgent.pathPending && distance < 0.5f) && Vector3.Distance(transform.position, player.transform.position) > alertArea) PatrolPattern();
        else
        {
            target = player;
            Attak(distance);
        }
    }

    private void Attak(float distance)
    {
        if (lasAttackCount > 0)
        {
            lasAttackCount -= Time.deltaTime;
        }
        else
        {
            if (distance < navAgent.stoppingDistance)
            {
                Hit();
                lasAttackCount = attackCooldown;
            }
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

    void Hit()
    {
        target.GetComponent<PlayerController>().TakeDamage(damage);
        Debug.Log("Attacked!");
    }

    void Die()
    {
        Destroy(gameObject);
    }
    void PatrolPattern()
    {
        patrolWP = (patrolWP + 1) % waypoints.Length;
        Seek(waypoints[patrolWP].transform.position);
    }

    void Seek(Vector3 targetPosition)
    {
        navAgent.destination = targetPosition;
    }
}
