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

    public Outlines RoomOutlines;
   
    // A list of all the rooms. The start room is at the beginning of the list,
    // and the end room is at the end of the list.
    private List<GameObject> rooms = new List<GameObject>();
    private int numRooms;

    private enum Dir { N = 0x1, E = 0x2, S = 0x4, W = 0x8 };

    // Todo: I could do all of this with just Dir and bitwise ops.
    // When I first wrote this, I didn't realize enums in C# were ever allowed
    // to have a value that wasn't explicitly defined.
    private enum ExitType
    {        
        N = Dir.N,
        E = Dir.E,
        S = Dir.S,
        W = Dir.W,
        ESW = Dir.E | Dir.S | Dir.W,
        EW = Dir.E | Dir.W,
        NE = Dir.N | Dir.E,
        NES = Dir.N | Dir.E | Dir.S,
        NESW = Dir.N | Dir.E | Dir.S | Dir.W,
        NEW = Dir.N | Dir.E | Dir.W,
        NS = Dir.N | Dir.S,
        NSW = Dir.N | Dir.S | Dir.W,
        NW = Dir.N | Dir.W,
        SE = Dir.S | Dir.E,
        SW = Dir.S | Dir.W
    }

    private void Awake()
    {
        numRooms = Random.Range(numRoomsMin, numRoomsMax + 1);
    }

    private void Start()
    {        
        GenerateLevel();
    }
   
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetLevelGeneration();
        }
    }

    private void GenerateLevel()
    {
        PlaceRooms();
        PlaceOutlines();
    }
  
    private void PlaceRooms()
    {
        PlaceRoom(0);

        for (int i = 1; i < numRooms; i++)
        {
            Vector3 nextCenter = ChooseCenter(currentCenter.position);
            currentCenter.position = nextCenter;
            PlaceRoom(i);
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
        
        rooms.Add(room);
    }

    // Choose a new room center based on the current.
    private Vector3 ChooseCenter(Vector3 currentCenter)
    {
        Dir next = ChooseNextDirection();

        Vector3 dir = MoveInCardinalDirection(next);

        Vector3 nextCenter = currentCenter;

        // Keep going in this direction even if we have to pass through rooms to do it.
        do
        {
            nextCenter += dir;
        }
        while (RoomExistsAt(nextCenter));

        return nextCenter;
    }
    
    // Convert a cardinal direction to a vector that translates
    // to a room center in that direction.
    private Vector3 MoveInCardinalDirection(Dir next)
    {
        float xOffset = 0f, yOffset = 0f;
        switch (next)
        {
            case Dir.N:
                yOffset = verticalDistanceBetweenRooms;
                break;
            case Dir.E:
                xOffset = horizontalDistanceBetweenRooms;
                break;
            case Dir.S:
                yOffset = -verticalDistanceBetweenRooms;
                break;
            case Dir.W:
                xOffset = -horizontalDistanceBetweenRooms;
                break;
        }
        return new Vector3(xOffset, yOffset, 0f);
    }

    // Randomly pick a cardinal direction to head in.
    private Dir ChooseNextDirection()
    {
        return (Dir)(1 << Random.Range(0, 4));
    }

    // Is there already a room at the given point? The point should be a room center.
    private Collider2D RoomExistsAt(Vector3 pt)
    {
        return Physics2D.OverlapCircle(pt, 0.2f, roomLayoutCollider);
    }

    // Place an outline around each room.
    private void PlaceOutlines()
    {
        foreach (GameObject room in rooms)
        {
            PlaceOutline(room);
        }
    }

    // Given a room, place an outline that creates exits to all the surrounding rooms.
    private void PlaceOutline(GameObject room)
    {
        Vector3 roomCenter = room.transform.position;
        // TODO: Figure out how to mishmash bitwise operators on two different enum types.
        int exitType = 0;

        foreach (Dir dir in Enum.GetValues(typeof(Dir)))
        {
            Vector3 pointToCheck = roomCenter + MoveInCardinalDirection(dir);
            if (RoomExistsAt(pointToCheck))
                exitType |= (int)dir;                
        }
        
        ExitType exitToPick = (ExitType)exitType;
        GameObject outline = PickOutline(exitToPick);
        Instantiate(outline, room.transform.position, room.transform.rotation);
    }

    // Returns the outline configured on this level generator for this type of exit.    
    private GameObject PickOutline(ExitType exitToPick)
    {
        // Todo: Would be nice to do this with a map
        // Todo: Either way, would be nice if the outlines class itself did this, but didn't
        // feel like sharing the ExitType enum out of the LevelGenerator. 
        switch(exitToPick)
        {
            case ExitType.N:
                return RoomOutlines.N;
            case ExitType.E:
                return RoomOutlines.E;
            case ExitType.S:
                return RoomOutlines.S;
            case ExitType.W:
                return RoomOutlines.W;
            case ExitType.ESW:
                return RoomOutlines.ESW;
            case ExitType.EW:
                return RoomOutlines.EW;
            case ExitType.NE:
                return RoomOutlines.NE;
            case ExitType.NES:
                return RoomOutlines.NES;
            case ExitType.NESW:
                return RoomOutlines.NESW;
            case ExitType.NEW:
                return RoomOutlines.NEW;
            case ExitType.NS:
                return RoomOutlines.NS;
            case ExitType.NSW:
                return RoomOutlines.NSW;
            case ExitType.NW:
                return RoomOutlines.NW;
            case ExitType.SE:
                return RoomOutlines.SE;
            case ExitType.SW:
                return RoomOutlines.SW;
        }
        throw new ArgumentException(
            string.Format("{0} is not a valid exit type.", exitToPick));
    }

    // Restart level generation by reloading this scene.
    private static void ResetLevelGeneration()
    {
        SceneManager.LoadScene("Generation Test");
    }
}

[Serializable]
public class Outlines
{
    public GameObject N, E, S, W, ESW, EW, NE, NES, NESW, NEW, NS, NSW, NW, SE, SW;
}