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
}
