using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D physics;
    public Animator animator;

    public GameObject[] deathSprites;
    public GameObject damageEffect;

    public float moveSpeed;
    public float rangeToChasePlayer;

    public int health;    

    void Start()
    {        
    }

    void Update()
    {
        Vector3 playerPos = PlayerController.instance.transform.position;
        Vector3 playerDir = playerPos - transform.position;
      
        if (playerDir.magnitude <= rangeToChasePlayer)
        {
            physics.velocity = playerDir.normalized * moveSpeed;
            animator.SetBool("isMoving", true);
        }
        else
        {
            physics.velocity = Vector3.zero;
            animator.SetBool("isMoving", false);
        }        
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        Instantiate(damageEffect, transform.position, transform.rotation);

        if (health > 0)
        {
            return;          
        }

        int splatterIndex = Random.Range(0, deathSprites.Length);
        int rotation = Random.Range(0, 4);

        Destroy(gameObject);
        Instantiate(
            deathSprites[splatterIndex],
            transform.position,
            Quaternion.Euler(0, 0, rotation * 90f)
        );              
    }
}
