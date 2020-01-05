using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorsCloseOnEnter;

    public GameObject[] doors;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.SetTarget(transform.position);
        }

        if (!doorsCloseOnEnter)
        {
            return;
        }

        foreach (var door in doors)
        {
            door.SetActive(true);
        }
    }
}
