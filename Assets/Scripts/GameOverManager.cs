using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject distanceCanvas;
    public TextMeshProUGUI finalScoreText;
    private bool isGameOver = false;

    public void TriggerGameOver()
{
    if (isGameOver) return;

    isGameOver = true;

    FindFirstObjectByType<DistanceTracker>()?.ShowFinalScore();

    Time.timeScale = 0f;
    gameOverUI.SetActive(true);
    distanceCanvas.SetActive(false);

    if (finalScoreText != null)
    {
         finalScoreText.text = "Final Score: " + ScoreManager.Instance.score.ToString();
    }

}


    public void RestartGame()
    {
        Time.timeScale = 1f; // Unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenus()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
