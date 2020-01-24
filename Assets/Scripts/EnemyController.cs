using UnityEngine;

public class EnemyController : MonoBehaviour
{        
    public GameObject[] deathSprites;
    public GameObject damageEffect;
    
    public int health;

    private void Update()
    {
        if (PlayerController.instance.transform.position.x < transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        Instantiate(damageEffect, transform.position, transform.rotation);

        if (health > 0)
        {
            AudioManager.instance.PlaySFX(SoundEffect.EnemyHurt);
            return;          
        }

        AudioManager.instance.PlaySFX(SoundEffect.EnemyDeath);
        Destroy(gameObject);

        if (deathSprites.Length == 0)
            return;

        int splatterIndex = Random.Range(0, deathSprites.Length);
        int rotation = Random.Range(0, 4);

        Instantiate(
            deathSprites[splatterIndex],
            transform.position,
            Quaternion.Euler(0, 0, rotation * 90f)
        );              
    }
}
