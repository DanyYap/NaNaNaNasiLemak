using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;
    public int lineIndex;

    [Header("Optional Model Change")]
    public bool changeModelOnTrigger = false;
    public int newModelIndex; // 0: Baby, 1: Student, etc.

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || dialogueData == null) return;

        if (other.CompareTag("Player"))
        {
            if (lineIndex >= 0 && lineIndex < dialogueData.lines.Length)
            {
                DialogueManager.Instance.PlayLine(dialogueData.lines[lineIndex]);

                if (changeModelOnTrigger)
                {
                    PlayerModelManager.Instance.SetModel(newModelIndex);
                }

                hasTriggered = true;
            }
            else
            {
                Debug.LogWarning($"Line index {lineIndex} is out of bounds in DialogueData.");
            }
        }
    }
}
