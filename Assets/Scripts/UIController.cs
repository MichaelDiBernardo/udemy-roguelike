using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider healthSlider;
    public Text healthText;
    public GameObject deathScreen;

    public Image levelFader;
    public float levelFadeTime;    

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

        levelFader.color = new Color(old.r, old.g, old.b, end);        
    }    
}
