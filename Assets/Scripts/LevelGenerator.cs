using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public int numRoomsMin, numRoomsMax;    
    public Color startRoomColor, endRoomColor;
    public float horizontalDistanceBetweenRooms;
    public float verticalDistanceBetweenRooms;

    public Transform currentCenter;

    public LayerMask roomLayoutCollider;
   
    // A list of all the rooms except the start and end rooms.
    private List<GameObject> rooms = new List<GameObject>();
    private int numRooms;

    private enum CardinalDirection { North, East, South, West };    

    private void Awake()
    {
        numRooms = Random.Range(numRoomsMin, numRoomsMax + 1);
    }

    private void Start()
    {        
        GenerateLevel();
    }

    private void GenerateLevel()
    {        
        PlaceRoom(0);

        for (int i = 1; i < numRooms; i++)
        {                 
            Vector3 nextCenter = ChooseCenter(currentCenter.position);
            currentCenter.position = nextCenter;
            PlaceRoom(i);                   
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetLevelGeneration();
        }
    }

    // Place the ith room at `currentCenter.`
    private void PlaceRoom(int roomIndex)
    {
        GameObject room = Instantiate(layoutRoom, currentCenter.position, currentCenter.rotation);

        if (roomIndex == 0)
            room.GetComponent<SpriteRenderer>().color = startRoomColor;
        else if (roomIndex == numRooms - 1)
            room.GetComponent<SpriteRenderer>().color = endRoomColor;
        else
            rooms.Add(room);
    }

    // Choose a new room center based on the current.
    private Vector3 ChooseCenter(Vector3 currentCenter)
    {
        CardinalDirection next = ChooseNextDirection();        

        Vector3 dir = MoveInCardinalDirection(next);

        Vector3 nextCenter = currentCenter;

        // Keep going in this direction even if we have to pass through rooms to do it.
        do
        {
            nextCenter += dir;
        }
        while (Physics2D.OverlapCircle(nextCenter, 0.2f, roomLayoutCollider));

        return nextCenter;
    }

    // Convert a cardinal direction to a vector that translates
    // to a room center in that direction.
    private Vector3 MoveInCardinalDirection(CardinalDirection next)
    {
        float xOffset = 0f, yOffset = 0f;
        switch (next)
        {
            case CardinalDirection.North:
                yOffset = verticalDistanceBetweenRooms;
                break;
            case CardinalDirection.East:
                xOffset = horizontalDistanceBetweenRooms;
                break;
            case CardinalDirection.South:
                yOffset = -verticalDistanceBetweenRooms;
                break;
            case CardinalDirection.West:
                xOffset = -horizontalDistanceBetweenRooms;
                break;
        }
        return new Vector3(xOffset, yOffset, 0f);
    }

    // Randomly pick a cardinal direction to head in.
    private CardinalDirection ChooseNextDirection()
    {
        return (CardinalDirection)Random.Range(
               (int)CardinalDirection.North,
               (int)CardinalDirection.West + 1
           );
    }

    // Restart level generation by reloading this scene.
    private static void ResetLevelGeneration()
    {
        SceneManager.LoadScene("Generation Test");
    }
}