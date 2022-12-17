using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorPrefab;
    [Space(10)]
    public int roomNumber;
    public GameObject doorInstance;
    [Space(10)]
    public Transform bottomDoorSpawns;
    public Transform topDoorSpawns;
    public Transform leftDoorSpawns;
    public Transform rightDoorSpawns;

    // Start is called before the first frame update
    void Start()
    {
        if (roomNumber == 1 || roomNumber == 8)
            spawnDoor(leftDoorSpawns.transform.GetChild(0).transform, leftDoorSpawns.transform.GetChild(1).transform, 1);
        else if(roomNumber == 2 || roomNumber == 3)
            spawnDoor(bottomDoorSpawns.transform.GetChild(0).transform, bottomDoorSpawns.transform.GetChild(1).transform, 0);
        else if(roomNumber == 4 || roomNumber == 5)
            spawnDoor(rightDoorSpawns.transform.GetChild(0).transform, rightDoorSpawns.transform.GetChild(1).transform, 1);
        else if(roomNumber == 6 || roomNumber == 7)
            spawnDoor(topDoorSpawns.transform.GetChild(0).transform, topDoorSpawns.transform.GetChild(1).transform, 0);
    }

    void spawnDoor(Transform p1, Transform p2, int dir) // dir = 0 for hor, 1 for ver
    {
        doorInstance = Instantiate(doorPrefab, transform);
        if(dir == 0)
        {
            doorInstance.transform.position = new Vector3(Random.Range(p1.position.x, p2.position.x), p1.position.y, p1.position.z);
        }
        else
        {
            doorInstance.transform.position = new Vector3(p1.position.x, Random.Range(p1.position.y, p2.position.y), p1.position.z);
        }
        doorInstance.transform.rotation = p1.rotation;
    }
}
