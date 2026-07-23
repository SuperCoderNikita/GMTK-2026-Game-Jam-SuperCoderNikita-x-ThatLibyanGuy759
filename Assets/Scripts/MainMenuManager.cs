using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void start()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void quit()
    {
        Application.Quit();
    }
}
