﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesClear;

    public List<GameObject> enemies = new List<GameObject>();

    [HideInInspector]
    public Room theRoom;

    // Start is called before the first frame update
    void Start()
    {
        if (openWhenEnemiesClear)
            theRoom.doorsCloseOnEnter = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(theRoom.PlayerIsHere() && openWhenEnemiesClear))
        {
            return;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
                i--;
            }
        }

        if (enemies.Count == 0)
        {
            theRoom.RemoveDoors();
        }

    }
}
