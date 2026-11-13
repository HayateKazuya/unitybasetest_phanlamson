using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Vent : MonoBehaviour, Interface
{
    [Header("Vent Settings")]
    public Transform ventdoor;       // Kéo cánh cửa Vent vào đây
    public float moveDistance = 10f; // Độ cao trượt vút lên
    public float moveSpeed = 5f;     // Tốc độ trượt

    private bool isOpening = false;
    private Vector3 targetPos;

    private void Start()
    {
        if (ventdoor != null)
        {
            targetPos = ventdoor.position + Vector3.up * moveDistance;
        }
    }

    private void Update()
    {
        if (isOpening && ventdoor != null)
        {
            ventdoor.position = Vector3.MoveTowards(ventdoor.position, targetPos, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(ventdoor.position, targetPos) < 0.05f)
            {
                ventdoor.gameObject.SetActive(false);
            }
        }
    }

    // Thực thi lệnh từ nút Interact bấm xuống
    public void Interact()
    {
        if (isOpening) return;
        isOpening = true;

        // Tắt va chạm để Char9 chui qua
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider c in allColliders)
        {
            c.enabled = false;
        }
    }
}