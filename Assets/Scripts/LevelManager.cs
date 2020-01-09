using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float delayBetweenLevels = 4f;

    public string nextLevel; 

    public Room CurrentRoom { get; set; }

    private void Awake()
    {
        instance = this;
    }

    public void EndLevel()
    {
        StartCoroutine(SwitchLevels(nextLevel));
    }

    private IEnumerator SwitchLevels(string levelName)
    {
        AudioManager.instance.PlayWinLevel();
        yield return new WaitForSeconds(delayBetweenLevels);
        SceneManager.LoadScene(levelName);
    }
}
