using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject gameOverPanel; // Kéo GameOverPanel vào đây

    private void Start()
    {
        // Đảm bảo vừa bấm Play là ẩn GameOver UI đi
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Control>(out Control playerControl))
        {
            playerControl.canMove = false;
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Swipe cameraSwipe = Camera.main.GetComponent<Swipe>();
            if (cameraSwipe != null)
            {
                cameraSwipe.enabled = false;
            }
    }

    public void ResetScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }
}