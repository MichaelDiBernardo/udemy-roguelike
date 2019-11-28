using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D physics;
    public float moveSpeed;
    public float rangeToChasePlayer;     

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
        }
        else
        {
            physics.velocity = Vector3.zero;
        }        
    }
}
