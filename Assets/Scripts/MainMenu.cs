using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string StartLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(StartLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
