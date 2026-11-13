using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Control : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 3f;
    public float gravity = -29.62f;

    public bool canMove = true;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!canMove) return;
        // 1. Reset trọng lực khi tiếp đất
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // 2. Đọc trực tiếp phím WASD từ Bàn Phím PC (New Input System)
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        float h = 0f;
        float v = 0f;

        if (kb.wKey.isPressed) v += 1f;
        if (kb.sKey.isPressed) v -= 1f;
        if (kb.dKey.isPressed) h += 1f;
        if (kb.aKey.isPressed) h -= 1f;

        Vector3 inputDir = new Vector3(h, 0f, v).normalized;
        Vector3 moveIntent = Vector3.zero;

        // 3. Tự động xoay và tính hướng di chuyển theo góc nhìn Camera
        if (inputDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;

            if (Camera.main != null)
            {
                targetAngle += Camera.main.transform.eulerAngles.y;
            }

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            moveIntent = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        // 4. Nhảy (Space)
        if (kb.spaceKey.wasPressedThisFrame && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 5. Cộng dồn trọng lực theo thời gian
        verticalVelocity += gravity * Time.deltaTime;

        // 6. Áp dụng chuyển động duy nhất 1 lần cho Character Controller
        Vector3 finalVelocity = (moveIntent * moveSpeed) + (Vector3.up * verticalVelocity);
        controller.Move(finalVelocity * Time.deltaTime);
    }
}