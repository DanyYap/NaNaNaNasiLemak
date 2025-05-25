using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Canvas mainMenuCanvas;
    public Button startButton;
    public Button quitButton;
    
    [Header("Gameplay UI")]
    public TextMeshProUGUI timerText; // Or use UnityEngine.UI.Text if not using TMP
    private float gameTime = 0f;
    private bool isGameActive = false;
    
    [Header("Pause Menu")]
    public GameObject pauseMenuCanvas;

    private bool isPaused = false;
    
    [Header("Cinemachine Cameras")]
    public CinemachineCamera menuCam;
    public CinemachineCamera gameplayCam;
    
    public static GameManager Instance; // already declared in most cases

    private void Awake()
    {
        Instance = this; // assign singleton
    }

    public float GetGameTime()
    {
        return gameTime;
    }
    
    private void Start()
    {
        // Set initial camera priorities
        menuCam.Priority = 20;
        gameplayCam.Priority = 10;

        // Disable gameplay input or player control here if needed

        // Set up button click
        startButton.onClick.AddListener(OnStartGame);
        quitButton.onClick.AddListener(OnQuitGame);
    }
    
    private void Update()
    {
        if (isGameActive)
        {
            gameTime += Time.deltaTime;
            UpdateTimerUI();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) PauseGame();
            else ResumeGame();
        }

        if (!isPaused && isGameActive)
        {
            gameTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }
    
    void UpdateTimerUI()
    {
        int hours = Mathf.FloorToInt(gameTime / 3600f);
        int minutes = Mathf.FloorToInt((gameTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);

        timerText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void OnQuitToMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DanyScene"); // Change to your menu scene name
    }

    void OnStartGame()
    {
        mainMenuCanvas.gameObject.SetActive(false);

        // Switch camera priorities to trigger blend
        gameplayCam.Priority = 20;
        menuCam.Priority = 10;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        isGameActive = true;
        gameTime = 0f;
    }
    
    void OnQuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Quit play mode in editor
        #endif
    }
}
