using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public int numRoomsMin, numRoomsMax;    
    public Color startRoomColor, endRoomColor;
    public float horizontalDistanceBetweenRooms;
    public float verticalDistanceBetweenRooms;

    public Transform currentCenter;

    private int numRooms;
    private enum CardinalDirection { North, East, South, West };    

    private void Awake()
    {
        numRooms = Random.Range(numRoomsMin, numRoomsMax + 1);
    }

    private void Start()
    {        
        PlaceRoom(0);

        for (int i = 1; i < numRooms; i++)
        {
            Vector3 nextCenter = ChooseCenter(currentCenter.position);
            currentCenter.position = nextCenter;            
            PlaceRoom(i);
        }
    }

    private void PlaceRoom(int roomIndex)
    {
        GameObject room = Instantiate(layoutRoom, currentCenter.position, currentCenter.rotation);

        if (roomIndex == 0)
            room.GetComponent<SpriteRenderer>().color = startRoomColor;
        else if (roomIndex == numRooms - 1)
            room.GetComponent<SpriteRenderer>().color = endRoomColor;
    }

    private Vector3 ChooseCenter(Vector3 currentCenter)
    {
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

        return currentCenter + new Vector3(xOffset, yOffset, 0f);
    }

    private CardinalDirection ChooseNextDirection()
    {
        return (CardinalDirection)Random.Range(
               (int)CardinalDirection.North,
               (int)CardinalDirection.West + 1
           );
    }
}
