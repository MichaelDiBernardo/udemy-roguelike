using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float invulnDuration;

    private FrameTimer _invulnTimer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        updateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (_invulnTimer != null && _invulnTimer.CheckThisFrame())
        {
            _invulnTimer = null;
            Color pc = PlayerController.instance.bodyRenderer.color;
            PlayerController.instance.bodyRenderer.color = new Color(pc.r, pc.g, pc.r, 1.0f);
                   
        }
    }

    public void DamagePlayer(int amount)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);                        
            LevelManager.instance.GameOver();
        }
        else
        {
            AudioManager.instance.PlaySFX(SoundEffect.PlayerHurt);
        }

        MakeInvulnerable(invulnDuration, true);        

        updateHealthUI();
    }

    public bool isInvulnerable
    {
        get { return _invulnTimer != null; }
    }

    public void MakeInvulnerable(float time, bool showIt)
    {
        if (isInvulnerable)
        {
            time = Mathf.Max(time, _invulnTimer.TimeLeft);
        }
        _invulnTimer = new FrameTimer(time, false);

        if (!showIt)
        {
            return;
        }

        Color pc = PlayerController.instance.bodyRenderer.color;
        PlayerController.instance.bodyRenderer.color = new Color(pc.r, pc.g, pc.r, 0.5f);
    }

    public void HealPlayer(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        updateHealthUI();
    }

    private void updateHealthUI()
    {
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = $"{currentHealth} / {maxHealth}";
    }
}
