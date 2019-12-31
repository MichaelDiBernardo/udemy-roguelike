using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    public float timeBeforeCollectable = 0.5f;

    private FrameTimer _collectTimer;
    private bool _isCollectable = false;

    // Start is called before the first frame update
    void Start()
    {
        _collectTimer = new FrameTimer(timeBeforeCollectable, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCollectable)
        {
            return;
        }

        _isCollectable = _collectTimer.CheckThisFrame();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isCollectable)
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
