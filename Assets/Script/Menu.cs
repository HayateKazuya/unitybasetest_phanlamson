using UnityEngine;

public class Menu : MonoBehaviour
{
    // static: giữ giá trị khi scene bị load lại (Reset sau khi chết)
    public static bool skipMenu = false;

    [Header("References")]
    public GameObject menuCanvas;
    public Control playerControl;
    public Swipe cameraSwipe;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (skipMenu)
        {
            // Vừa Reset sau khi chết: bỏ qua menu, vào chơi luôn
            skipMenu = false;
            menuCanvas.SetActive(false);
            SetGameplay(true);
        }
        else
        {
            menuCanvas.SetActive(true);
            SetGameplay(false);
        }
    }

    public void PlayGame()
    {
        menuCanvas.SetActive(false);
        SetGameplay(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetGameplay(bool enabled)
    {
        if (playerControl != null) playerControl.canMove = enabled;
        if (cameraSwipe != null) cameraSwipe.enabled = enabled;
    }
}