using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score { get; private set; }
    public int live { get; private set; }
    public int waveID;
    public int totalWave;
    public bool isBossLive;
    public bool isBigBoss;
    public bool isGameOver;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        GameOver();
    }

    public void NewGame()
    {
        live = 3;
        score = 0;
        Time.timeScale = 1;
        isGameOver = false;
    }

    private void GameOver()
    {
        StartCoroutine(gameOver());
    }

    private IEnumerator gameOver()
    {
        if (live == 0)
        {
            SoundManager.Instance.GameOverSfx();
            isGameOver = true;
            yield return new WaitForSeconds(3f);
            NewGame();
        }
    }

    public void AddScore(int enemyScore)
    {
        this.score += enemyScore;
    }

    public void AddLive(int extraLive)
    {
        live += extraLive;
    }

    public void MinusLive(int minus)
    {
        live -= minus;
    }
}
