using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Slider healthSlider;
    public Text healthText;    

    public Image levelFader;
    public float levelFadeTime;

    public string mainMenuScene;

    public GameObject pauseMenu;

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public void FadeLevelIn()
    {
        StartCoroutine(Fader(false));
    }

    public void FadeLevelOut()
    {
        StartCoroutine(Fader(true));
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ShowPauseScreen()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pauseMenu.SetActive(false);
    }

    public void ResumeFromPause()
    {
        LevelManager.instance.TogglePaused();
    }

    private IEnumerator Fader(bool toBlack)
    {
        float increment = 0.05f;
        float delay = increment * levelFadeTime;

        float alpha = toBlack ? 0f : 1f;
        float end = toBlack ? 1f : 0f;

        Color old = levelFader.color;        
                
        while (!Mathf.Approximately(alpha, end))
        {
            alpha = Mathf.MoveTowards(alpha, end, increment);
            levelFader.color = new Color(old.r, old.g, old.b, alpha);
            yield return new WaitForSeconds(delay);
        }

        // Paranoia: If the alpha isn't precisely 0 or 1 because of floating-point math,
        // just force-set it before leaving it behind.
        levelFader.color = new Color(old.r, old.g, old.b, end);        
    }    
}
