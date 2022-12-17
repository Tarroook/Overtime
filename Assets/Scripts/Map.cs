using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Room currentRoom;

    public List<GameObject> allRooms;
    public List<GameObject> rooms;
    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<GameObject>();
        generateMap();
        openRoom(1);
    }

    void generateMap()
    {
        for (int i = 0; i < 8; i++)
        {
            int rand = Random.Range(0, allRooms.Count - 1);
            rooms.Add(Instantiate(allRooms[rand], gameObject.transform));
            //allRooms.RemoveAt(rand);
        }

        for(int r = 0; r < rooms.Count; r++)
        {
            rooms[r].GetComponent<Room>().roomNumber = r + 1;
            rooms[r].SetActive(false);
        }
    }

    void openRoom(int roomNb)
    {
        Debug.Log("Opened room " + roomNb);
        GameObject room = rooms[roomNb - 1];
        room.SetActive(true);
        currentRoom = room.GetComponent<Room>();
    }
}
