using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.TitleScreen();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void TwoPlayers()
    {
        SceneManager.LoadScene("SelectMap");
    }
}
