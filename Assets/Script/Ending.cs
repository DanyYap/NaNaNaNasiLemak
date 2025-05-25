using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ending : MonoBehaviour
{
    public Animator coffinAnimator;      // Assign in Inspector for Trigger1
    public GameObject endingUI;          // Assign in Inspector for Trigger2
    public TextMeshProUGUI timeText;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (gameObject.name == "Trigger1")
        {
            if (coffinAnimator != null)
                coffinAnimator.SetTrigger("Enter");
            else
                Debug.LogWarning("Coffin Animator not assigned on Trigger1.");
        }
        else if (gameObject.name == "Trigger2")
        {
            if (endingUI != null)
            {
                endingUI.SetActive(true);

                // Get time from GameManager and display it
                float time = GameManager.Instance.GetGameTime(); // We'll define this method below
                int hours = Mathf.FloorToInt(time / 3600);
                int minutes = Mathf.FloorToInt((time % 3600) / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                timeText.text = $"Time: {hours:D2}:{minutes:D2}:{seconds:D2}";
            }
            else
            {
                Debug.LogWarning("Ending UI not assigned on Trigger2.");
            }

            // unlock & show mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Optional: pause the game
            Time.timeScale = 0f;
        }
    }
}
