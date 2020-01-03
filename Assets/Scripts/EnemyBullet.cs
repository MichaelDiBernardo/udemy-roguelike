﻿using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {       
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Why aren't enemy bullets colliding with scenery? Must be a component issue.
        bool destroyMe = true;
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
            if (PlayerController.instance.IsDashing)
            {
                destroyMe = false;
            }
        }

        if (destroyMe)
        {
            AudioManager.instance.PlaySFX(SoundEffect.Impact);
            Destroy(gameObject);
        }
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
