using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
     *  Chi tiết
     *  Nhân vật:(Done)
     *      - Di chuyển kiểu top-down (Done)
     *      - Nhấn nút space sẽ thả ra 1 quả bom ngay dưới chân. Có khả năng đẩy quả bom theo hướng nhất định.(Done)
     *      - Đụng phải tia lửa của bom sẽ mất 1 mạng.
     *      - Có 3 mạng, hết 3 mạng sẽ kết thúc (Chế độ 1 người).
     *      - Có animation khi di chuyển, khi chết. (Done)
     *  
     *  Quả bom:(Done)
     *      - Phát nổ sau khoảng 3s.(Done)
     *      - Khi phát nổ sẽ tỏa 2 tia lửa theo 4 hướng (ban đầu tia lửa sẽ chiếm 1 ô).(Done)
     *      - Tia lửa có khả năng phá hủy các khối brick.(Done)
     *      - Có animation trước và sau khi phát nổ.(Done)
     *      
     *  Kẻ địch(Done)
     *      - Tự di chuyển(theo hướng ngang dọc).   (Thiết kế thêm khả năng tránh quả bom nhưng không tuyệt đối)
     *      - Bị tiêu diệt bởi các tia lửa từ quả bom.
     *      - Có animation khi di chuyển, khi chết.
     *  
     *  Power up(Done)
     *      - Có tổng cộng 3 power up.
     *          + Tăng tốc độ(Speed): tốc độ người chơi sẽ tăng thêm 1 đơn vị.
     *          + Thêm mạng(Live): cộng thêm 1 mạng cho người chơi.
     *          + Tăng kích cỡ vụ nổ(Blast): các quả bom do người chơi thả ra sẽ có vụ nổ to hơn trước 1 đơn vị.
     *      - Các power up sẽ ẩn dưới các khối brick, xuất hiện 1 cách random.
     *  
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
    public int stage;
    public float time { get; private set; }
    public static GameManager instance { get; private set; }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
        EnemiesScan();
    }

    private void Update()
    {
        EnemiesScan();
    }

    private void FixedUpdate()
    {
        time -= 1 * Time.fixedDeltaTime;
    }

    public void NewGame()
    {
        time = 180f;
        this.stage = 1;
    }

    //public void LoadStage()
    //{
    //    EnemiesScan();
    //    if (this.totalEnemies <= 0 && this.time >= 0)
    //    {
    //        this.stage++;
    //        SceneManager.LoadScene("Level" + this.stage);
    //    } 
    //}

    private void EnemiesScan()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>();
        totalEnemies = enemy.Length;
    }
}
