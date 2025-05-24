using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void PlayLine(DialogueData.DialogueLine line)
    {
        if (line == null) return;

        dialogueText.text = line.text;
        dialoguePanel.SetActive(true);

        if (line.audioClip != null)
        {
            audioSource.clip = line.audioClip;
            audioSource.Play();
            CancelInvoke(nameof(HidePanel));
            Invoke(nameof(HidePanel), line.audioClip.length + 0.5f);
        }
        else
        {
            CancelInvoke(nameof(HidePanel));
            Invoke(nameof(HidePanel), 4f);
        }
    }

    private void HidePanel()
    {
        dialoguePanel.SetActive(false);
        audioSource.Stop();
    }
}
