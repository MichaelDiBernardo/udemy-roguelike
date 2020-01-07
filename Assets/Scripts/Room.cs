using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorsCloseOnEnter;
    public bool openWhenEnemiesClear;

    public GameObject[] doors;

    public List<GameObject> enemies = new List<GameObject>();    

    void Update()
    {
        if (!(PlayerIsHere() && openWhenEnemiesClear))
        {
            return;
        }
                
        for (int i=0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
                i--;
            }
        }

        if (enemies.Count == 0)
        {
            RemoveDoors();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        LevelManager.instance.CurrentRoom = this;
        MoveCameraToHere();
        MaybeActivateDoors();
    }   

    private void MaybeActivateDoors()
    {
        if (!doorsCloseOnEnter)
        {
            return;
        }

        foreach (var door in doors)
        {
            door.SetActive(true);
        }
    }

    private void MoveCameraToHere()
    {
        CameraController.instance.SetTarget(transform.position);
    }

    private void RemoveDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false);
        }
        doorsCloseOnEnter = false;
    }

    private bool PlayerIsHere()
    {
        return LevelManager.instance.CurrentRoom == this;
    }
}