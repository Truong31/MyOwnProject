using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform playerPosition;
    public GameObject pausePanel;
    public int score { get; private set; }
    public int live { get; private set; }


    /*TODO:
     *     - Viet them ve Enemies (Tha ra cac vien dan o vi tri bat ky) (Done)
     *     
     *     - Viet them ve Power (Xuat hien ngau nhien sau khi tieu diet duoc 1 Enemy). Bao gom 3 Power:
     *          + Them la chan (Done)
     *          + Tang so vien dan ban ra   (Done)
     *          + Them mang (Done)
     *          + Xu ly ve cach xuat hien cua cac Power, tan suat xuat hien cua moi Power (Done)
     *     - Cach di chuyen de Enemy khong vuot ra ngoai man hinh (Done)
     *     
     *     
     *     - Them cac Wave khac:
     *          + Cac enemy xuat hien thanh hinh chu nhat (Done)
     *          + Cac enemy xuat hien thanh hinh tron (Done)
     *          + Cac enemy xuat hien thanh hang (Done)
     *          + Cac enemy xuat hien thanh hang, di theo duong cheo, doi huong moi khi va vao thanh(Done)
     *          + Cac khoi thien thach roi (Keo dai khoang 10s) tu goc tren ben trai, roi cheo. (Done)
     *              Chung co 3 kich thuoc tuong ung so vien dan co the chiu(To nhat chiu duoc 3 vien va giam dan)
     *          + Het 4 Wave se xuat hien 1 MiniBoss(Di chuyen lung tung, tha ra cac vien dan co the toa ra thanh nhieu vien khac) (Done)
     *          + Cach chuyen doi giua cac Wave, cac Wave se co thu tu 1, 2, 3 ... (Done)
     *          + Se co tong cong 4 dot, moi dot gom 4 Wave thuong va 1 Wave MiniBoss
     *          + Dot cuoi sau khi vuot qua 4 Wave se suat hien Boss chinh
     *          
     *      - Viet them ve Boss chinh:
     *          + Viet lai Healthbar (Done)
     *          + Co 100 mau (co the thay doi them de phu hop)
     *          + Ban dau se tha ra cac vien dan giong MiniBoss (Done)
     *          + Khi con 60 mau se tha ra cac ten lua co kha nang duoi theo nguoi choi(Ten lua chiu duoc 1 phat ban cua nguoi choi) (Done)
     *          + Khi con 30 mau se tha ra cac qua min co kha nang phat no dien rong sau 1 khoang thoi gian.
     *          O trong pham vi vu no se chet (90%. Sửa lại khi bắn trúng, bomb không bị hất lên)
     *          
     *      - Them man hinh bat dau tro choi. Bao gom:
     *          + 1 nut bat dau(Giua man hinh) (Done)
     *          + 1 nut Option(Dieu chinh am luong, tat tieng) (Done)
     *          
     *      - Them man hinh Pause. Bao gom 3 nut:
     *          + 1 nut New Game
     *          + 1 nut Continue
     *          + 1 nut Option (Giong man hinh bat dau)
     *          
     *      - Them Sound, Text
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

    private void Update()
    {
        PauseGame();
    }

    public void NewGame()
    {
        live = 3;
        score = 0;

    }

    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    public void ResumeGame()
    {
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
}
