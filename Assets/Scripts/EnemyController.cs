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

    public bool shouldShoot;
    public float fireRate;
    public float shootRange;

    public GameObject bullet;
    public Transform muzzlePoint;    

    private FrameTimer _shotTimer;

    void Start()
    {
        _shotTimer = new FrameTimer(fireRate, true);    
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

        if (!shouldShoot)
        {
            return;
        }

        float playerDistance = Vector3.Distance(
            transform.position,
            PlayerController.instance.transform.position
        );

        if (playerDistance > shootRange)
        {
            _shotTimer.Reset();
            return;
        }
                
        if (_shotTimer.CheckThisFrame())
        {            
            Instantiate(bullet, muzzlePoint.position, muzzlePoint.rotation);
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
