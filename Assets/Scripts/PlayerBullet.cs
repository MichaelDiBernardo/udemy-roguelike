using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D physics;

    void Start()
    {
        // Video directed us to put this in Update() -- why?
        physics.velocity = transform.right * speed;
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
