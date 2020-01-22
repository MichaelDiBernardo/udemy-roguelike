using UnityEngine;

public class EnemyRunner : EnemyMover
{
    public float moveSpeed;
    public float rangeToFleePlayer;
        
    protected override bool MoveThisFrame()
    {        
        Vector3 playerPos = PlayerController.instance.transform.position;
        Vector3 playerDir = transform.position - playerPos;

        if (playerDir.magnitude <= rangeToFleePlayer)
        {
            _physics.velocity = playerDir.normalized * moveSpeed;
            return true;
        }
        else
        {
            _physics.velocity = Vector3.zero;
            return false;
        }
    }    
}
