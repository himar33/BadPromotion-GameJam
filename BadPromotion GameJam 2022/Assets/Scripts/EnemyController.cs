using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent navAgent;
    public int alertArea;

    enum TypeEnemy { walker, flying };
    enum State { patrolling, attaking };

    State state;
    [SerializeField] TypeEnemy typeEnemy;


    [SerializeField] int life = 2;
    [SerializeField] int damage = 5;

    [SerializeField] float attackCooldown = 1.5f;
    float lasAttackCount = 0f;

    [SerializeField] ParticleSystem deadPartSys;

    public GameObject[] waypoints;
    int patrolWP;

    Material material;

    // Start is called before the first frame update
    void Start()
    {        
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = waypoints[0].transform.position;
        navAgent.stoppingDistance = 1.5f;
        patrolWP = waypoints.Length;
        state = State.patrolling;

        deadPartSys.Pause();

        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0) Die();
        
        float distance = Mathf.Abs(navAgent.remainingDistance);

        //Patrol
        if (Mathf.Abs(Vector3.Distance(player.transform.position, navAgent.transform.position)) > alertArea)
        {
            if (!navAgent.pathPending && distance < navAgent.stoppingDistance)
            {
                material.DisableKeyword("_EMISSION");
                PatrolPattern();
            }
        }
        else
        {
            material.EnableKeyword("_EMISSION");
            Attack(distance);
        }
       
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

    private void Attack(float distance)
    {
       // if (state == State.patrolling) ChangeState();
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
        Instantiate(deadPartSys, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void PatrolPattern()
    {
        //if (navAgent.remainingDistance < 0.5)
            patrolWP = (patrolWP+1) % waypoints.Length;
         Seek(waypoints[patrolWP].transform.position);
    }

    void Seek(Vector3 targetPosition)
    {
        navAgent.destination = targetPosition;
    }
}
