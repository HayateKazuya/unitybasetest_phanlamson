using UnityEngine;
using UnityEngine.InputSystem;

public class Swipe_Alt : MonoBehaviour
{
    public Transform target;
    public float distance = 8.83f;
    public Vector3 targetOffset = new Vector3(0f, 0.15f, 0f);
    public float sensitivity = 0.2f;

    public float minPitch = -20f; 
    public float maxPitch = 60f;  

    private float currentYaw = 0f;    
    private float currentPitch = 15f; 

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Nhận input vuốt
        Vector2 delta = Vector2.zero;
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            delta = Touchscreen.current.primaryTouch.delta.ReadValue();
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
            delta = Mouse.current.delta.ReadValue();

        // 2. Tính góc xoay
        if (delta.sqrMagnitude > 0.01f)
        {
            currentYaw += delta.x * sensitivity;
            currentPitch -= delta.y * sensitivity;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        }

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 targetPos = target.position + targetOffset;
        Vector3 desiredPos = targetPos - (rotation * Vector3.forward * distance);

        // 3. TỐI GIẢN: Bắn 1 tia Linecast, tự động BỎ QUA các vùng Trigger (Vent/DeathZone)
        if (Physics.Linecast(targetPos, desiredPos, out RaycastHit hit, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            // Nếu đụng tường/sàn -> Đặt camera tại điểm đụng (nhích lại gần nhân vật 0.1f để không dính sát vách)
            transform.position = hit.point + (targetPos - hit.point).normalized * 0.1f;
        }
        else
        {
            transform.position = desiredPos;
        }

        transform.LookAt(targetPos);
    }
}