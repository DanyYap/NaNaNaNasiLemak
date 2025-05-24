using UnityEngine;

public class PlayerModelManager : MonoBehaviour
{
    public static PlayerModelManager Instance;

    [Header("Player Models in Order (Baby → OldMan)")]
    public GameObject[] playerModels; // 0: Baby, 1: Student, 2: Adult, 3: OldMan

    private int currentModelIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        SetModel(0); // Start with Baby
    }

    public void SetModel(int index)
    {
        if (index < 0 || index >= playerModels.Length)
        {
            Debug.LogWarning($"Invalid model index: {index}");
            return;
        }

        for (int i = 0; i < playerModels.Length; i++)
        {
            playerModels[i].SetActive(i == index);
        }

        currentModelIndex = index;
    }

    public int GetCurrentModelIndex() => currentModelIndex;
}
