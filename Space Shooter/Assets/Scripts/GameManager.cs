using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score { get; private set; }
    public int live { get; private set; }
    public bool isBossLive;


    /*TODO:        
     *      - Them Text
     *      - Hien thi so thu tu cua Wave khi bat dau wave
     *      - Them panel game over (Game over thi quay ve Main Menu), ket thuc tro choi
     *          
     *
     */

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

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        live = 3;
        score = 0;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
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
