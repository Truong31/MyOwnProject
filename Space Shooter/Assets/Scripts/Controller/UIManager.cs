using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI gameoverText;
    public GameObject pauseMenu;

    private void Start()
    {
        gameoverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        PauseGame();
        SettingLive();
        SettingScore();
        StartCoroutine(GameOver());
        
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

    public IEnumerator GameOver()
    {
        if (GameManager.Instance.isGameOver)
        {
            gameoverText.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);
            MainMenu();
        }
    }

    private void SettingScore()
    {
        float score = GameManager.Instance.score;
        scoreText.text = score + "";
    }

    private void SettingLive()
    {
        float live = GameManager.Instance.live;
        liveText.text = live + "";
    }
}
