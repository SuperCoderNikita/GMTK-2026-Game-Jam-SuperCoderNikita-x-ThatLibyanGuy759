using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public ScoreManager score;
    void Update()
    {
        if(score.score == 0)
        {
            gameOverScreen.SetActive(true);
        }
    }
    public void retry()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void quit()
    {
        Application.Quit();
    }
}
