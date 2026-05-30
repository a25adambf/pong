using UnityEngine;
using TMPro;
 
public class GameManager : MonoBehaviour
{
    int p1Score;
    int p2Score;
    bool running = false;

    [Header("Score UI")]
    [SerializeField] TMP_Text txtP1Score;
    [SerializeField] TMP_Text txtP2Score;

    [Header("Game Objects")]
    [SerializeField] GameObject pelota;

    [Header("Win Condition")]
    [SerializeField] int pointsToWin = 10;

    [Header("References")]
    [SerializeField] UIManager uiManager;

    void Start()
    {
        Cursor.visible = false;

        // Initialize UI
        if (uiManager != null)
        {
            uiManager.ShowTitleScreen();
    }
 
    void Update()
    {
        // Escape to quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    }

    /// <summary>
    /// Called by UIManager when player presses Space on title screen
    /// </summary>
    public void StartGame()
    {
        // Reset scores
        p1Score = 0;
        p2Score = 0;
        UpdateScoreDisplay();

        // Activate the ball
        pelota.SetActive(true);
        running = true;

        // Play start sound
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayGameStart();
    }

    /// <summary>
    /// Called by UIManager when player presses Space on game over screen
    /// </summary>
    public void RestartGame()
    {
        // Stop the ball
        pelota.SetActive(false);

        // Reset scores
        p1Score = 0;
        p2Score = 0;
        UpdateScoreDisplay();

        // Reset the ball's position and state
        PelotaController ball = pelota.GetComponent<PelotaController>();
        if (ball != null)
        {
            ball.ResetBall();
        }

        // Deactivate ball (will be reactivated by StartGame)
        pelota.SetActive(false);
        running = false;
    }
 
    public void AddPointP1()
    {
        p1Score++;
        UpdateScoreDisplay();

        // Play goal sound
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayGoal();

        // Check win condition
        if (p1Score >= pointsToWin)
        {
            EndGame();
        }
    }
 
    public void AddPointP2()
    {
        p2Score++;
        UpdateScoreDisplay();

        // Play goal sound
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayGoal();

        // Check win condition
        if (p2Score >= pointsToWin)
        {
            EndGame();
        }
    }

    private void UpdateScoreDisplay()
    {
        if (txtP1Score != null)
            txtP1Score.text = p1Score.ToString();
        if (txtP2Score != null)
            txtP2Score.text = p2Score.ToString();
    }

    private void EndGame()
    {
        running = false;

        // Stop the ball
        pelota.SetActive(false);

        // Update high scores
        int highScore = 0;
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.CheckAndSaveMatchScore(p1Score, p2Score);
            HighScoreManager.Instance.UpdatePlayerHighScore(1, p1Score);
            HighScoreManager.Instance.UpdatePlayerHighScore(2, p2Score);
            highScore = HighScoreManager.Instance.GetHighScore();
        }

        // Show game over screen
        if (uiManager != null)
        {
            uiManager.ShowGameOver(p1Score, p2Score, highScore);
        }
    }

    public bool IsRunning()
    {
        return running;
    }
}
