using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject EndPanel;

    private void Start()
    {
        if (EndPanel != null)
        {
            EndPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Control>(out Control playerControl))
        {
            playerControl.canMove = false;
            TriggerEnd();
        }
    }

    private void TriggerEnd()
    {
        if (EndPanel != null)
        {
            EndPanel.SetActive(true);
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