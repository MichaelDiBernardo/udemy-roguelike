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

    public void DamagePlayer()
    {
        if (isInvulnerable)
        {
            return;
        }

        currentHealth--;

        if (currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
            UIController.instance.deathScreen.SetActive(true);
        }

        _invulnTimer = new FrameTimer(invulnDuration, false);

        Color pc = PlayerController.instance.bodyRenderer.color;
        PlayerController.instance.bodyRenderer.color = new Color(pc.r, pc.g, pc.r, 0.5f);

        updateHealthUI();
    }

    public bool isInvulnerable
    {
        get { return _invulnTimer != null; }
    }

    private void updateHealthUI()
    {
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = $"{currentHealth} / {maxHealth}";
    }
}
