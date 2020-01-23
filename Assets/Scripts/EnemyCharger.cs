using UnityEngine;

public class EnemyCharger : EnemyMover
{
    public float startMoveSpeed, topMoveSpeed;
    public float accelerationPerSecond;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = startMoveSpeed;        
    }

    protected override void MoveThisFrame()
    {
        Vector3 playerPos = PlayerController.instance.transform.position;
        Vector3 playerDir = playerPos - transform.position;

        if (currentSpeed < topMoveSpeed)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                topMoveSpeed,
                accelerationPerSecond * Time.deltaTime
            );
        }                

        _physics.velocity = playerDir.normalized * currentSpeed;
    }
}
