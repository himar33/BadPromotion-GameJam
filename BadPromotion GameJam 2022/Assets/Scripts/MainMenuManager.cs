using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private AudioClip menuMusic;
    private AudioManager audio;

    private void Awake()
    {
        audio = GetComponent<AudioManager>();
        audio.PlayClip(menuMusic);
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
