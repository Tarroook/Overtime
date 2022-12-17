using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<GameObject> allRooms;
    List<GameObject> rooms;
    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<GameObject>();
        generateMap();
    }

    void generateMap()
    {
        for (int i = 0; i < 8; i++)
        {
            int rand = Random.Range(0, allRooms.Count - 1);
            Debug.Log(rand + "list : " + allRooms[rand]);
            rooms.Add(allRooms[rand]);
            //allRooms.RemoveAt(rand);
        }

        for(int r = 0; r < rooms.Count; r++)
        {
            GameObject roomInstance = Instantiate(rooms[r], gameObject.transform);
            roomInstance.GetComponent<Room>().roomNumber = r + 1;
            if(r != 0)
                roomInstance.SetActive(false);
        }
    }
}
