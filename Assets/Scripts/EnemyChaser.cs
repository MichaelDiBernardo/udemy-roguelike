using UnityEngine;

public class EnemyChaser : EnemyMover
{
    public float moveSpeed;
    public float rangeToChasePlayer;    

    protected override void MoveThisFrame()
    {
        Vector3 playerPos = PlayerController.instance.transform.position;
        Vector3 playerDir = playerPos - transform.position;

        if (playerDir.magnitude <= rangeToChasePlayer)
        {
            _physics.velocity = playerDir.normalized * moveSpeed;                        
        }
        else
        {
            _physics.velocity = Vector3.zero;            
        }
    }
}
