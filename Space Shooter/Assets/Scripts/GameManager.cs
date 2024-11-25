using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
     *          + Cac enemy xuat hien thanh hinh ZigZag
     *          + Cac khoi thien thach roi (Keo dai khoang 10s) tu goc tren ben trai, roi cheo. 
     *              Chung co 3 kich thuoc tuong ung so vien dan co the chiu(To nhat chiu duoc 3 vien va giam dan)
     *          + Het 4 Wave se xuat hien 1 MiniBoss(Di chuyen lung tung, tha ra cac vien dan co the toa ra thanh nhieu vien khac)
     *          + Cach chuyen doi giua cac Wave, giua luc chuyen do se hien la Wave bao nhieu
     *          + Cac Wave se co thu tu 1-1, 1-2, 2-1, 2-2....
     *          + Se co tong cong 4 dot, moi dot gom 4 Wave thuong va 1 Wave MiniBoss
     *          + Dot cuoi sau khi vuot qua 4 Wave se suat hien Boss chinh
     *          
     *      - Viet them ve Boss chinh:
     *          + Co 100 mau (co the thay doi them de phu hop)
     *          + Ban dau se dung im, tha ra cac vien dan giong MiniBoss, nhung nhieu hon
     *          + Khi con 80 mau se bat dau di chuyen lung tung
     *          + Khi con 60 mau se tha ra cac ten lua co kha nang duoi theo nguoi choi(Ten lua chiu duoc 3 phat ban cua nguoi choi)
     *          + Khi con 30 mau se tha ra cac qua min co kha nang phat no dien rong sau 1 khoang thoi gian. O trong pham vi vu no se chet
     *          
     *      - Them man hinh bat dau tro choi. Bao gom:
     *          + 1 nut bat dau(Giua man hinh)
     *          + 1 nut Option(Dieu chinh am luong, tat tieng)
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

    public void NewGame()
    {
        live = 3;
        score = 0;

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
