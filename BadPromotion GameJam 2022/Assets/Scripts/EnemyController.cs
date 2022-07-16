using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent navAgent;
    public int alertArea;

    enum TypeEnemy { walker, flying };
    enum State { patrolling, attaking };

    State state;
    [SerializeField] TypeEnemy typeEnemy;


    [SerializeField] int life = 2;
    [SerializeField] int damage = 5;

    [SerializeField] float attackCooldown = 1.5f;
    float lasAttackCount = 0f;


    public GameObject[] waypoints;
    int patrolWP = 4;


    // Start is called before the first frame update
    void Start()
    {        
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = waypoints[0].transform.position;
        patrolWP = waypoints.Length;
        state = State.patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0) Die();
        
        float distance = navAgent.remainingDistance;

        if(Vector3.Distance(navAgent.transform.position, player.transform.position) < alertArea)
        Debug.Log("AAAAAAAAAALEEEEEEEEEEEEEEEERTAAAAAAAAAAAAAAAAAAAAA");
        Debug.Log("transform.position; " + navAgent.transform.position);
        Debug.Log("player.transform.position; " + player.transform.position);
        //Patrol
        if ((!navAgent.pathPending && distance < 0.5f) && Vector3.Distance(navAgent.transform.position, player.transform.position) > alertArea)
        {
            if(state == State.attaking) ChangeState();
            PatrolPattern();
        }
        else Attak(distance);
       
    }

    private void ChangeState()
    {
        switch (state)
        {
            case State.patrolling:
                state = State.attaking;
                navAgent.ResetPath();
                break;
            case State.attaking:
                state = State.patrolling;
                navAgent.ResetPath();
                break;
            default:
                break;
        }
       
    }

    private void Attak(float distance)
    {
        if (state == State.patrolling) ChangeState();
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
        if (navAgent.destination != player.transform.position)
        {
            navAgent.destination = player.transform.position;
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
        player.GetComponent<PlayerController>().TakeDamage(damage);
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
