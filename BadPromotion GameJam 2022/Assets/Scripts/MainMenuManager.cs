using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private AudioClip menuMusic;
    private AudioManager audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioManager>();
    }

    private void Start()
    {
        audioSource.PlayClip(menuMusic);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
