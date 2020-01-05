using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorsCloseOnEnter;
    public bool openWhenEnemiesClear;

    public GameObject[] doors;

    public List<GameObject> enemies = new List<GameObject>();

    private bool _playerInRoom;

    void Update()
    {
        if (!_playerInRoom || !openWhenEnemiesClear)
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

        _playerInRoom = true;
        MoveCameraToHere();
        MaybeActivateDoors();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRoom = false;
        }
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
}