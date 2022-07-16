using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject target;
    NavMeshAgent navAgent;

    [SerializeField] int life = 20;
    [SerializeField] int damage = 5;

    [SerializeField] float attackCooldown = 1.5f;
    float lasAttackCount = 0f;

    bool firstFrame = true;

    // Start is called before the first frame update
    void Start()
    {        
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Die();
        }

        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > 5)
            return;

        if (firstFrame)
        {
            navAgent.destination = target.transform.position;
            navAgent.stoppingDistance = 2.1f;
        }

        if (lasAttackCount > 0)
        {
            lasAttackCount -= Time.deltaTime;
        }
        else
        {
            if (distance < navAgent.stoppingDistance)
            {
                Attack();
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

    void Attack()
    {
        target.GetComponent<PlayerController>().TakeDamage(damage);
        Debug.Log("Attacked!");
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
