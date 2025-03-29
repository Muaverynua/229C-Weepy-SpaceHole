using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject distanceCanvas;
    private bool isGameOver = false;

    public void TriggerGameOver()
{
    if (isGameOver) return;

    isGameOver = true;

    FindFirstObjectByType<DistanceTracker>()?.ShowFinalScore();

    Time.timeScale = 0f;
    gameOverUI.SetActive(true);
    distanceCanvas.SetActive(false);
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
