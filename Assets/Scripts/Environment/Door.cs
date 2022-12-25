using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool isOpened = false;
    private Map map;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    }

    private void Update()
    {
        if (isOpened)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public override void interact()
    {
        if (isOpened)
            goToNextRoom();
        else
            doorIsClosed();
    }

    void goToNextRoom() 
    {
        if(Map.currentRoom.roomNumber == 8)
            map.openRoom(1);
        else
            map.openRoom(Map.currentRoom.roomNumber + 1);
    }

    void doorIsClosed()
    {
        // play an animation / sound
    }
}
