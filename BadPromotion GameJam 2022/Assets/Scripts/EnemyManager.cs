using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform player;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        //int randNum = Random.Range(0, 2);
        GameObject enemySpawned = Instantiate(enemy);
        do
        {
            float x = gameObject.transform.position.x;
            float z = gameObject.transform.position.z;
            enemySpawned.transform.position = new Vector3(Random.Range(x + 30.0f, x + 90.0f), 1.0f, 0);
        } while ((enemySpawned.transform.position - player.position).magnitude <= 20.0f);
    }
}
