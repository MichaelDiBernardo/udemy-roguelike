using UnityEngine;

public class PlayerBullet : MonoBehaviour
{    
    public Rigidbody2D physics;
    public GameObject impactEffect;    

    public float speed = 7.5f;
    public int attackPower = 50;

    void Start()
    {
        // Video directed us to put this in Update() -- why?
        physics.velocity = transform.right * speed;
    }

    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        Destroy(gameObject);

        if (other.CompareTag("Enemy"))
        {            
            other.GetComponent<EnemyController>().DamageEnemy(attackPower);            
        }
        else
        {
            AudioManager.instance.PlaySFX(SoundEffect.Impact);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }
}
