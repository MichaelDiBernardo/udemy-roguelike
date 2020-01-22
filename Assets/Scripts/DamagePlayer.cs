using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TryToHurt(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryToHurt(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        TryToHurt(other.collider);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        TryToHurt(other.collider);
    }

    private void TryToHurt(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
        }
    }
}
