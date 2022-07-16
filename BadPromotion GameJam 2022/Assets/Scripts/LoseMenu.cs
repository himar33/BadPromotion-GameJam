using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
