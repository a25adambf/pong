using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameOverPanel;

    [Header("Title Panel Texts")]
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text controlsText;
    [SerializeField] TMP_Text startText;

    [Header("Game Over Panel Texts")]
    [SerializeField] TMP_Text winnerText;
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text restartText;

    [Header("References")]
    [SerializeField] GameManager gameManager;

    void Start()
    {
        ShowTitleScreen();
    }

    void Update()
    {
        // Handle input on title screen
        if (titlePanel != null && titlePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }

        // Handle input on game over screen
        if (gameOverPanel != null && gameOverPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    public void ShowTitleScreen()
    {
        if (titlePanel != null) titlePanel.SetActive(true);
        if (gamePanel != null) gamePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (titleText != null) titleText.text = "PONG";
        if (controlsText != null)
            controlsText.text = "JUGADOR 1: W (arriba) / S (abajo)\n\nJUGADOR 2: ↑ (arriba) / ↓ (abajo)\n\nPRIMERO EN LLEGAR A 10 GANA";
        if (startText != null) startText.text = "PULSA ESPACIO PARA JUGAR\n\nESC PARA SALIR";
    }

    public void StartGame()
    {
        if (titlePanel != null) titlePanel.SetActive(false);
        if (gamePanel != null) gamePanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // Play start sound
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayGameStart();

        // Tell game manager to start
        if (gameManager != null)
            gameManager.StartGame();
    }

    public void ShowGameOver(int p1Score, int p2Score, int highScore)
    {
        if (titlePanel != null) titlePanel.SetActive(false);
        if (gamePanel != null) gamePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        string winner = p1Score > p2Score ? "JUGADOR 1" : "JUGADOR 2";
        if (winnerText != null) winnerText.text = "¡GANADOR!" + winner;
        if (finalScoreText != null) finalScoreText.text = p2Score + " - " + p1Score;
        if (restartText != null) restartText.text = "PULSA ESPACIO PARA REINICIAR";
    }


    public void HideAll()
    {
        if (titlePanel != null) titlePanel.SetActive(false);
        if (gamePanel != null) gamePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    private void RestartGame()
    {
        // Play button click sound
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayButtonClick();

        // Tell game manager to restart
        if (gameManager != null)
            gameManager.RestartGame();

        // Show title screen
        ShowTitleScreen();
    }
}