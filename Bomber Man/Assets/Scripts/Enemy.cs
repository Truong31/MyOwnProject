using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float moveSpeed = 2.5f;
    public LayerMask obstacleLayer; // LayerMask cho các vật cản (tường, bom, ...)
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right }; // Các hướng di chuyển có thể
    private Vector2 currentDirection; // Hướng di chuyển hiện tại
    private Vector2 targetPosition; // Vị trí mục tiêu trên lưới để di chuyển tới
    private bool isMoving = false; // Kiểm tra xem enemy đang di chuyển hay không

    [Header("Sprite Animation")]
    public SpriteAnimation spriteAnimationUp;
    public SpriteAnimation spriteAnimationDown;
    public SpriteAnimation spriteAnimationLeft;
    public SpriteAnimation spriteAnimationRight;
    public SpriteAnimation spriteAnimationDeath;

    private void Start()
    {
        // Khởi tạo hướng di chuyển ban đầu ngẫu nhiên
        currentDirection = directions[Random.Range(0, directions.Length)];
        targetPosition = transform.position; // Bắt đầu tại vị trí hiện tại
    }

    private void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(Move());
        }
        Animation(currentDirection);
    }

    private IEnumerator Move()
    {
        isMoving = true;

        // 1. Kiểm tra xem có thể di chuyển tiếp theo hướng hiện tại không
        if (CanMoveInDirection(currentDirection))
        {
            targetPosition = (Vector2)transform.position + currentDirection;
        }
        else
        {
            // 2. Nếu không thể di chuyển tiếp, chọn hướng di chuyển mới
            currentDirection = ChooseNewDirection();
            if (CanMoveInDirection(currentDirection))
            {
                targetPosition = (Vector2)transform.position + currentDirection;
            }
            else
            {
                // Nếu không có hướng nào khả thi, có thể đợi một chút hoặc có logic khác
                isMoving = false;
                yield break; // Dừng coroutine nếu không có đường đi
            }
        }

        // 3. Thực hiện di chuyển mượt mà đến vị trí mục tiêu
        float sqrRemainingDistance = (transform.position - (Vector3)targetPosition).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - (Vector3)targetPosition).sqrMagnitude;
            yield return null; // Đợi frame tiếp theo
        }
        transform.position = targetPosition; // Đảm bảo đến chính xác vị trí target
        isMoving = false;
    }

    // Hàm chọn hướng di chuyển mới
    private Vector2 ChooseNewDirection()
    {
        List<Vector2> possibleDirections = new List<Vector2>(directions);
        possibleDirections.Remove(currentDirection * -1); // Tránh quay đầu ngay lập tức (có thể tùy chỉnh)

        List<Vector2> validDirections = new List<Vector2>();
        foreach (Vector2 dir in possibleDirections)
        {
            if (CanMoveInDirection(dir))
            {
                validDirections.Add(dir);
            }
        }

        if (validDirections.Count > 0)
        {
            return validDirections[Random.Range(0, validDirections.Count)]; // Chọn ngẫu nhiên một hướng hợp lệ
        }
        else
        {
            // Nếu không có hướng nào hợp lệ (ví dụ như bị kẹt), quay đầu lại hoặc giữ nguyên
            return currentDirection * -1;
        }
    }

    // Hàm kiểm tra xem có thể di chuyển theo hướng chỉ định hay không
    private bool CanMoveInDirection(Vector2 direction)
    {
        Vector2 checkPosition = (Vector2)transform.position + direction;
        Collider2D hit = Physics2D.OverlapCircle(checkPosition, 0.2f, obstacleLayer); // Kiểm tra va chạm tại vị trí mới
        return hit == null;

    }

    //Các Animation tương ứng với hướng di chuyển
    private void Animation(Vector2 direction)
    {
        spriteAnimationDown.enabled = direction == Vector2.down;
        spriteAnimationUp.enabled = direction == Vector2.up;
        spriteAnimationLeft.enabled = direction == Vector2.left;
        spriteAnimationRight.enabled = direction == Vector2.right;

    }

    //Hành vi của enemy khi đụng phải tia lửa của bomb
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            currentDirection = Vector2.zero;
            spriteAnimationDown.enabled = false;
            spriteAnimationUp.enabled = false;
            spriteAnimationLeft.enabled = false;
            spriteAnimationRight.enabled = false;
            spriteAnimationDeath.enabled = true;

            Destroy(this.gameObject, 1);
        }
    }
}
