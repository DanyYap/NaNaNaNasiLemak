using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Canvas mainMenuCanvas;
    public Button startButton;

    [Header("Cinemachine Cameras")]
    public CinemachineCamera menuCam;
    public CinemachineCamera gameplayCam;

    private void Start()
    {
        // Set initial camera priorities
        menuCam.Priority = 20;
        gameplayCam.Priority = 10;

        // Disable gameplay input or player control here if needed

        // Set up button click
        startButton.onClick.AddListener(OnStartGame);
    }

    void OnStartGame()
    {
        mainMenuCanvas.gameObject.SetActive(false);

        // Switch camera priorities to trigger blend
        gameplayCam.Priority = 20;
        menuCam.Priority = 10;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
