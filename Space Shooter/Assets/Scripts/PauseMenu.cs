using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Update()
    {
        PauseGame();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
