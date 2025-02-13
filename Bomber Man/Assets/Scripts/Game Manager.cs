using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*TODO: Sửa lại khâu mở level mới. Cụ thể nếu hoàn thành level 1, 2 sau đó chơi chơi lại và hoàn thành level 1 thì level 3 vẫn mở
            Xem lại VictoryDoor
    */
    public int totalEnemies { get; private set; }
    public float time { get; private set; }
    public TextMeshProUGUI timeText;
    public GameObject pausePanel;

    private void Start()
    {
        time = 180f;
        Time.timeScale = 1;
        EnemiesScan();
        SoundManager.instance.BackGround();
    }

    private void Update()
    {
        EnemiesScan();
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            PauseGame();
        }
    }

    private void FixedUpdate()
    {
        if(time > 0)
        {
            time -= 1 * Time.fixedDeltaTime;   
        }
        else if(time <= 0)
        {
            time = 0;
        }
        timeText.text = Mathf.CeilToInt(this.time) + "";
    }

    private void EnemiesScan()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>();
        totalEnemies = enemy.Length;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
