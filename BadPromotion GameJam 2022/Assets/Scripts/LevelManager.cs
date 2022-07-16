using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LoseCondition();
    }

    void LoseCondition()
    {
        if (player.GetComponent<PlayerController>().GetLife() <= 0)
            SceneManager.LoadScene(1);
    }

    // Win Condition
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            SceneManager.LoadScene(1);
    }
}
