using UnityEngine;

public class Ending : MonoBehaviour
{
    public Animator coffinAnimator;      // Assign in Inspector for Trigger1
    public GameObject endingUI;          // Assign in Inspector for Trigger2

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Trigger1: play coffin animation
        if (gameObject.name == "Trigger1")
        {
            if (coffinAnimator != null)
                coffinAnimator.SetTrigger("Enter");
            else
                Debug.LogWarning("Coffin Animator not assigned on Trigger1.");
        }
        // Trigger2: show ending UI, unlock cursor, pause game
        else if (gameObject.name == "Trigger2")
        {
            if (endingUI != null)
                endingUI.SetActive(true);
            else
                Debug.LogWarning("Ending UI not assigned on Trigger2.");

            // unlock & show mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
