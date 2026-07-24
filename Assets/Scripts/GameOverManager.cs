using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public ScoreManager score;
    void Update()
    {
        if (score.score <= 0 && !gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Game");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void startTime()
    {
        Time.timeScale = 1f;
    }
}
