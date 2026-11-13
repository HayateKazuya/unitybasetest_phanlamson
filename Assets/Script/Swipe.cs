using UnityEngine;
using UnityEngine.InputSystem;

public class Swipe : MonoBehaviour
{
    [SerializeField] private Transform target; // Kéo Char9 vào đây

    public float distance = 8.83f;
    public float height = 3.12f;
    [Header("Target Offset")]
    public Vector3 targetOffset = new Vector3(0f, 0.15f, 0f);
    public float sensitivity = 0.2f;

    // Góc xoay
    private float currentYaw = 0f;    // Xoay trái / phải
    private float currentPitch = 15f; // Xoay lên / xuống (mặc định 15 độ)

    // Giới hạn góc ngước/cúi (chặn không bị chui lọt đất hoặc lật ngược camera)
    public float minPitch = -20f; // Góc âm: Hạ camera thấp nhìn ngước lên chân
    public float maxPitch = 60f;  // Góc dương: Đưa camera lên cao nhìn đỉnh đầu

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Nhận input cảm ứng (Touch) hoặc Giữ chuột phải/chuột trái vuốt (New Input System)
        Vector2 delta = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            delta = Touchscreen.current.primaryTouch.delta.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            delta = Mouse.current.delta.ReadValue();
        }

        // 2. Cộng dồn góc xoay theo thao tác vuốt
        if (delta.sqrMagnitude > 0.01f)
        {
            currentYaw += delta.x * sensitivity;
            currentPitch -= delta.y * sensitivity; // Vuốt xuống -> Camera hạ thấp nhìn lên chân

            // Giới hạn góc Pitch để vuốt xuống chân mượt mà không bị lọt nền
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        }


        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

        // Tâm nhìn thay vì lấy cố định height trên đầu, ta dùng targetOffset
        Vector3 targetPosition = target.position + targetOffset;

        // Đẩy camera ra sau
        Vector3 finalPosition = targetPosition - (rotation * Vector3.forward * distance);

        transform.position = finalPosition;
        transform.LookAt(targetPosition);
    }
}