using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Sleeper
{
    public float spawnTime;
    public GameObject thingToSpawn;

    private FrameTimer _spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = new FrameTimer(spawnTime, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAwake && _spawnTimer.CheckThisFrame())
        {
            Instantiate(thingToSpawn, transform.position, transform.rotation);
        }
    }
}
