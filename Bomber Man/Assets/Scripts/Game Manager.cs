using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*  Cơ chế game
     *  - Người chơi điều khiển nhân vật, thả các quả bom để tiêu diệt kẻ thù.
     *  - Có 2 chế độ chơi
     *      + Chế độ 1 người chơi: Bao gồm 7 màn
     *          + Điều khiển nhân vật (có tất cả 4 nhân vật, có thể tùy chọn 1 nhân vật để điều khiển).
     *          + Ở mỗi màn chơi sẽ có 1 số lượng kẻ địch nhất định cùng 1 cổng ra (ở vị trí bất kỳ).
     *           Tiêu diệt hết kẻ địch cổng sẽ mở. Đi qua cổng để kết thúc màn chơi.
     *      + Chế độ 2 - 4 người chơi
     *          + Mỗi người điều khiển một người chơi (tự chọn). Người chiến thắng là người cuối cùng còn sống.
     *          + Có thể chọn các map bất kỳ để chơi.
     *  
     *  Scene
     *      - 1 Scene màn hình chính:
     *          + 1 nút để vào chế độ 1 người chơi.
     *          + 1 nút vào chế độ nhiều người chơi.
     *          + 1 nút cài đặt
     *              + Cài đặt âm thanh, hiệu ứng.
     *              + Cài đặt nút điều khiển(Mặc định chế độ 1 người chơi, nút điều khiển sẽ cài đặt theo người chơi số 1 trong chế độ nhiều người chơi).
     *          + 1 nút thoát game.
     *      - 1 Scene màn hình chờ chế độ 1 người chơi(Khi nhấn nút 1 người chơi)
     *          + Chọn nhân vật chơi
     *          + Nút start để bắt đầu
     *      - 1 Scene màn hình chờ chế độ nhiều người chơi(Khi nhấn nút nhiều người chơi)
     *          + Chọn số lượng người chơi
     *          + Chọn nhân vật cho từng người chơi
     *          + Chọn map
     *          + Nút bắt đầu
     *      - Với chế độ 1 người chơi: gồm 7 Scene là lần lượt các màn chơi.
     *      - Với chế độ nhiều người chơi: gồm 4 Scene là các map cho người chơi chọn.
     *      - 1 Scene Kết thúc trò chơi.
     *      - 1 Panel khi tạm dừng trò chơi.
     * 
     *  UX/UI: thêm các âm thanh, hiệu ứng, text.
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
