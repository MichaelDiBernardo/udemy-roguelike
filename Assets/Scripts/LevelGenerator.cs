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

    public int numTriesToPlaceRoom;

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
        Debug.Log("Starting level generation!");

        PlaceRoom(0);

        for (int i = 1; i < numRooms; i++)
        {            
            try
            {
                Vector3 nextCenter = ChooseCenter(currentCenter.position, numTriesToPlaceRoom);
                currentCenter.position = nextCenter;
                PlaceRoom(i);
            }
            catch (CouldNotGenerateLevelException)
            {
                Debug.Log("Restarting level generation...");
                ResetLevelGeneration();
            }             
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetLevelGeneration();
        }
    }

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

    private Vector3 ChooseCenter(Vector3 currentCenter, int tries)
    {
        if (tries < 0)
        {
            throw new CouldNotGenerateLevelException("Ran out of tries placing room.");
        }

        CardinalDirection next = ChooseNextDirection();        

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

        Vector3 nextCenter = currentCenter + new Vector3(xOffset, yOffset, 0f);

        if (Physics2D.OverlapCircle(nextCenter, 0.2f, roomLayoutCollider))
        {
            return ChooseCenter(currentCenter, tries - 1);
        }

        return nextCenter;
    }

    private CardinalDirection ChooseNextDirection()
    {
        return (CardinalDirection)Random.Range(
               (int)CardinalDirection.North,
               (int)CardinalDirection.West + 1
           );
    }

    private static void ResetLevelGeneration()
    {
        SceneManager.LoadScene("Generation Test");
    }
}

[Serializable]
public class CouldNotGenerateLevelException : Exception
{
    public CouldNotGenerateLevelException()
    { }

    public CouldNotGenerateLevelException(string message)
        : base(message)
    { }

    public CouldNotGenerateLevelException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
