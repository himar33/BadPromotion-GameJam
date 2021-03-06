using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hud;
    public FadeToBlack fadeToBlack;

    [SerializeField] private AudioClip levelMusic;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        audioManager.PlayClip(levelMusic, true);
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
            StartCoroutine(fadeToBlack.LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }

    // Win Condition
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (hud != null) hud.SetActive(false);

            if (SceneManager.GetActiveScene().name == "Level2")
                StartCoroutine(fadeToBlack.LoadLevel(0));
            else
                StartCoroutine(fadeToBlack.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

}
