using UnityEngine;
using UnityEngine.InputSystem;

public class Swipe : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;                  
    [SerializeField] private CharacterController targetController; 

    [Header("Camera")]
    public float distance = 8.83f;
    public Vector3 targetOffset = new Vector3(0f, 0.15f, 0f);
    public float sensitivity = 0.2f;

    private float currentYaw = 0f;
    private float currentPitch = 15f;

    [Header("Swipe")]
    public float minPitch = -20f;
    public float maxPitch = 60f;

    private bool hasLanded = false;

    void LateUpdate()
    {
        if (target == null) return;

        Vector2 delta = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            delta = Touchscreen.current.primaryTouch.delta.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            delta = Mouse.current.delta.ReadValue();
        }

        if (delta.sqrMagnitude > 0.01f)
        {
            currentYaw += delta.x * sensitivity;
            currentPitch -= delta.y * sensitivity;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        }

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 targetPosition = target.position + targetOffset;
        Vector3 finalPosition = targetPosition - (rotation * Vector3.forward * distance);

        transform.position = finalPosition;
        transform.LookAt(targetPosition);
    }
}