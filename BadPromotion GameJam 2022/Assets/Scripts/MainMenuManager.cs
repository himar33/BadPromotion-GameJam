using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public FadeToBlack fadeToBlack;

    [SerializeField]private AudioClip menuMusic;
    private AudioManager audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioManager>();
    }

    private void Start()
    {
        audioSource.PlayClip(menuMusic, true);
    }

    public void PlayGame()
    {
        StartCoroutine(fadeToBlack.LoadLevel(1));
        //SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
