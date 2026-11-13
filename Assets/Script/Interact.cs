using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject interactButton; // Kéo Nút UI Interact trên Canvas vào đây 1 lần duy nhất

    private Interface currentInteractable;

    private void Start()
    {
        // Ban đầu ẩn nút UI đi
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
    }

    // Hàm này gán duy nhất 1 LẦN vào On Click () của nút InteractButton trên Canvas
    public void OnInteractButtonPressed()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            
            // Tương tác xong thì ẩn nút UI đi và xóa mục tiêu hiện tại
            if (interactButton != null) interactButton.SetActive(false);
            currentInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Khi Char9 đi vào vùng va chạm của bất kỳ vật thể nào có IInteractable
        Interface interactable = other.GetComponent<Interface>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            if (interactButton != null) interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Khi Char9 đi ra xa khỏi vật thể tương tác
        Interface interactable = other.GetComponent<Interface>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            if (interactButton != null) interactButton.SetActive(false);
        }
    }
}