using System.Collections;
using UnityEngine;

public class EnemyWander : EnemyMover
{
    public float wanderSpeedMin, wanderSpeedMax;
    public float wanderTimeMin, wanderTimeMax;
    public float idleTimeMin, idleTimeMax;    
    
    void Start()
    {
        StartCoroutine(Wander());
    }

    protected override void MoveThisFrame()
    {
        // Do nothing: The coroutine does everything.
        // Which is bad, I know.
        // This means the coroutine does stuff and a frame later or so, the base class might turf the movement.
        // This means that offscreen wanderers are probably moving 1 frame and stopping once in a while. Oh well.
        // Todo: Fix this.
    }

    private IEnumerator Wander()
    {
        float pauseBeforeStarting = Random.Range(0.2f, 0.5f);
        yield return new WaitForSeconds(pauseBeforeStarting);

        while(true)
        {
            float moveSpeed = Random.Range(wanderSpeedMin, wanderSpeedMax);          
            Vector3 wanderDirection = new Vector3(
                Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

            _physics.velocity = wanderDirection.normalized * moveSpeed;
            
            float wanderTime = Random.Range(wanderTimeMin, wanderTimeMax);            
            yield return new WaitForSeconds(wanderTime);

            float idleTime = Random.Range(wanderTimeMin, wanderTimeMax);
            _physics.velocity = Vector3.zero;            
            yield return new WaitForSeconds(idleTime);
        }
    }
}
