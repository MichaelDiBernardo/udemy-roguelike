using UnityEngine;

public class EnemyController : MonoBehaviour
{        
    public GameObject[] deathSprites;
    public GameObject damageEffect;
    
    public int health;
        
    public void DamageEnemy(int damage)
    {
        health -= damage;
        Instantiate(damageEffect, transform.position, transform.rotation);

        if (health > 0)
        {
            AudioManager.instance.PlaySFX(SoundEffect.EnemyHurt);
            return;          
        }

        int splatterIndex = Random.Range(0, deathSprites.Length);
        int rotation = Random.Range(0, 4);

        AudioManager.instance.PlaySFX(SoundEffect.EnemyDeath);
        Destroy(gameObject);
        Instantiate(
            deathSprites[splatterIndex],
            transform.position,
            Quaternion.Euler(0, 0, rotation * 90f)
        );              
    }
}
