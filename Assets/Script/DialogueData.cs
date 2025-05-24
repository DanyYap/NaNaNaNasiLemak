using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue System/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 5)] public string text;
        public AudioClip audioClip;
    }

    public DialogueLine[] lines;
}
