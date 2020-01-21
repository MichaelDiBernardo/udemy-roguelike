using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorsCloseOnEnter;    

    public GameObject[] doors;

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
    
    public void RemoveDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false);
        }
        doorsCloseOnEnter = false;
    }

    public bool PlayerIsHere()
    {
        // TODO: Awkward spot with level generation, remove later.
        if (LevelManager.instance == null)
            return false;
        return LevelManager.instance.CurrentRoom == this;
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


}