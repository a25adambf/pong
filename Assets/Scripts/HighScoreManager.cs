using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }

    private const string HIGH_SCORE_KEY = "HighScore";
    private const string HIGH_SCORE_P1_KEY = "HighScoreP1";
    private const string HIGH_SCORE_P2_KEY = "HighScoreP2";

    private int highScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    public int GetHighScore()
    {
        return highScore;
    }

    /// <summary>
    /// Updates the high score if the given score is higher.
    /// Returns true if a new high score was set.
    /// </summary>
    public bool TryUpdateHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the best score for a specific player.
    /// </summary>
    public int GetPlayerHighScore(int player)
    {
        string key = player == 1 ? HIGH_SCORE_P1_KEY : HIGH_SCORE_P2_KEY;
        return PlayerPrefs.GetInt(key, 0);
    }

    /// <summary>
    /// Updates a player's individual best score.
    /// </summary>
    public void UpdatePlayerHighScore(int player, int score)
    {
        string key = player == 1 ? HIGH_SCORE_P1_KEY : HIGH_SCORE_P2_KEY;
        int current = PlayerPrefs.GetInt(key, 0);
        if (score > current)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Resets all high scores.
    /// </summary>
    public void ResetHighScores()
    {
        highScore = 0;
        PlayerPrefs.DeleteKey(HIGH_SCORE_KEY);
        PlayerPrefs.DeleteKey(HIGH_SCORE_P1_KEY);
        PlayerPrefs.DeleteKey(HIGH_SCORE_P2_KEY);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Calculates the combined score (total points) for high score comparison.
    /// The high score is based on the maximum total points reached in a match.
    /// </summary>
    public void CheckAndSaveMatchScore(int p1Score, int p2Score)
    {
        int totalPoints = p1Score + p2Score;
        int matchWinnerScore = Mathf.Max(p1Score, p2Score);
        TryUpdateHighScore(matchWinnerScore);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}