using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;

    [SerializeField] GameObject menuUi;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        menuUi.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void Pause()
    {
        menuUi.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
