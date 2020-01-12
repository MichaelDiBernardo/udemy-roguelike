using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float delayBetweenLevels = 4f;

    public string nextLevel, gameOverScene; 

    public Room CurrentRoom { get; set; }

    public bool IsPaused { get; set; }    

    private void Awake()
    {
        instance = this;        
    }

    private void Start()
    {
        Time.timeScale = 1f;
        UIController.instance.FadeLevelIn();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePaused();
        }
    }

    public void EndLevel()
    {
        StartCoroutine(SwitchLevels(nextLevel));
    }

    public void TogglePaused()
    {         
        if (IsPaused)
        {
            UIController.instance.HidePauseScreen();
            IsPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            UIController.instance.ShowPauseScreen();
            IsPaused = true;
            Time.timeScale = 0f;
        }        
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    private IEnumerator SwitchLevels(string levelName)
    {
        AudioManager.instance.PlayWinLevel();        
        UIController.instance.FadeLevelOut();
        PlayerController.instance.CanMove = false;

        yield return new WaitForSeconds(delayBetweenLevels);

        SceneManager.LoadScene(levelName);        
    }
}
