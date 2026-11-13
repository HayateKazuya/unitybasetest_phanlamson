using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
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
        Menu.skipMenu = true;
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }
}