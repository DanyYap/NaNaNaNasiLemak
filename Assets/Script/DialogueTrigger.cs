using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;
    public int lineIndex;

    [Header("Optional Model Change")]
    public bool changeModelOnTrigger = false;
    public int newModelIndex; // 0: Baby, 1: Student, etc.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueData != null && lineIndex >= 0 && lineIndex < dialogueData.lines.Length)
            {
                DialogueManager.Instance.PlayLine(dialogueData.lines[lineIndex]);
            }
        }
    }
}
