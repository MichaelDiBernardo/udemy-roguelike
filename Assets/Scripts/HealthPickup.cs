using System.Collections;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    public float timeBeforeCollectable = 0.5f;    
    private bool _isCollectable = false;
    
    void Start()
    {
        StartCoroutine(SetCollectable(timeBeforeCollectable));
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isCollectable)
        {
            AudioManager.instance.PlaySFX(SoundEffect.PickupHealth);
            PlayerHealthController.instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }

    private IEnumerator SetCollectable(float afterSeconds)
    {
        yield return new WaitForSeconds(afterSeconds);
        _isCollectable = true;
    }
}
