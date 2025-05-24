using UnityEngine;

public class PlayerModelSwitcher : MonoBehaviour
{
    public Transform player; // Assign your player transform here (drag in Inspector)
    public float[] heightThresholds; // Define heights for Baby, Student, Adult, OldMan (in order)

    private int currentModelIndex = 0;

    void Update()
    {
        float currentY = player.position.y;

        for (int i = heightThresholds.Length - 1; i >= 0; i--)
        {
            if (currentY >= heightThresholds[i])
            {
                if (currentModelIndex != i)
                {
                    currentModelIndex = i;
                    PlayerModelManager.Instance.SetModel(i);
                }
                return;
            }
        }

        // If player falls below lowest threshold
        if (currentModelIndex != 0)
        {
            currentModelIndex = 0;
            PlayerModelManager.Instance.SetModel(0);
        }
    }
}
