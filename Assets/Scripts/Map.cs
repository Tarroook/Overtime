using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Room currentRoom;
    public static int maxRooms = 8;
    public List<GameObject> allRooms;
    public static List<GameObject> rooms;

    public List<GameObject> defaultEnemyList;

    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<GameObject>();
        generateMap();
        openRoom(1);
    }

    void generateMap()
    {
        for (int i = 0; i < maxRooms; i++)
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

    public void openRoom(int roomNb)
    {
        if(currentRoom != null)
        {
            currentRoom.gameObject.SetActive(false);
        }
        Debug.Log("Opened room " + roomNb);
        GameObject room = rooms[roomNb - 1];
        room.SetActive(true);
        currentRoom = room.GetComponent<Room>();
    }
}
