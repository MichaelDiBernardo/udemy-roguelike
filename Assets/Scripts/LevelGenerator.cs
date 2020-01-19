using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public int distanceToEnd;
    public Color startRoomColor, endRoomColor;

    public Transform currentCenter;

    private void Start()
    {
        GameObject firstRoom = Instantiate(layoutRoom, currentCenter.position, currentCenter.rotation);
        firstRoom.GetComponent<SpriteRenderer>().color = startRoomColor;
    }
}
