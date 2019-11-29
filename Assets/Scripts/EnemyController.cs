using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D physics;
    public Animator animator;

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

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
