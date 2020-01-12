using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public float TimeToWaitForInstructions = 2f;
    public GameObject Instructions;
    public string MainMenuScene;

    private bool _canExit = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(DelayInstructions());        
    }

    void Update()
    {
        if (_canExit && Input.anyKeyDown)
        {
            SceneManager.LoadScene(MainMenuScene);
        }
    }

    public IEnumerator DelayInstructions()
    {
        yield return new WaitForSeconds(TimeToWaitForInstructions);
        Instructions.SetActive(true);
        _canExit = true;
    }
}
