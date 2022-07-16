using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hud;
    public Animator transition;
    public float transitionTime = 1.0f;

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
        {
            hud.SetActive(false);
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
            //Destroy(player);
        }
    }

    // Win Condition
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (hud != null) hud.SetActive(false);

            if (SceneManager.GetActiveScene().name == "SampleScene")
                StartCoroutine(LoadLevel(0));
            else
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
                        
        //StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("FadingIn");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
}
