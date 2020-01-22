using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : EnemyMover
{
    public float wanderSpeedMin, wanderSpeedMax;
    public float wanderTimeMin, wanderTimeMax;
    public float idleTimeMin, idleTimeMax;
    private bool moving;
    
    void Start()
    {
        StartCoroutine(Wander());
    }

    protected override bool MoveThisFrame()
    {
        return moving;
    }

    private IEnumerator Wander()
    {
        float pauseBeforeStarting = Random.Range(0.2f, 2f);
        yield return new WaitForSeconds(pauseBeforeStarting);

        while(true)
        {
            float moveSpeed = Random.Range(wanderSpeedMin, wanderSpeedMax);          
            Vector3 wanderDirection = new Vector3(
                Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

            _physics.velocity = wanderDirection.normalized * moveSpeed;
            
            float wanderTime = Random.Range(wanderTimeMin, wanderTimeMax);
            moving = true;
            yield return new WaitForSeconds(wanderTime);

            float idleTime = Random.Range(wanderTimeMin, wanderTimeMax);
            _physics.velocity = Vector3.zero;
            moving = false;
            yield return new WaitForSeconds(idleTime);
        }
    }
}
