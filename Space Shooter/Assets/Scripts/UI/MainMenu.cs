using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.BeginningSound();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        SoundManager.Instance.PlayingSound();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
