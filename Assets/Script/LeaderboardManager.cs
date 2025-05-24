using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;

    private const int maxEntries = 5;

    public void SaveNewTime(float newTime)
    {
        for (int i = 0; i < maxEntries; i++)
        {
            string key = "Time_" + i;
            if (!PlayerPrefs.HasKey(key) || newTime < PlayerPrefs.GetFloat(key))
            {
                InsertNewScore(i, newTime);
                break;
            }
        }
        PlayerPrefs.Save();
        ShowLeaderboard();
    }

    private void InsertNewScore(int index, float newTime)
    {
        for (int i = maxEntries - 1; i > index; i--)
        {
            PlayerPrefs.SetFloat("Time_" + i, PlayerPrefs.GetFloat("Time_" + (i - 1)));
        }
        PlayerPrefs.SetFloat("Time_" + index, newTime);
    }

    public void ShowLeaderboard()
    {
        leaderboardText.text = "Top Times:\n";
        for (int i = 0; i < maxEntries; i++)
        {
            if (PlayerPrefs.HasKey("Time_" + i))
            {
                float time = PlayerPrefs.GetFloat("Time_" + i);
                int minutes = Mathf.FloorToInt(time / 60f);
                int seconds = Mathf.FloorToInt(time % 60f);
                leaderboardText.text += $"{i + 1}. {minutes:00}:{seconds:00}\n";
            }
        }
    }
}
