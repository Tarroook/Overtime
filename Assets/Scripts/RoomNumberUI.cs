using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomNumberUI : MonoBehaviour
{
    private int previousRoomNumber;
    private TextMeshProUGUI textMesh;
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Map.currentRoom.roomNumber != previousRoomNumber)
        {
            textMesh.SetText("Room " + Map.currentRoom.roomNumber.ToString());
            previousRoomNumber = Map.currentRoom.roomNumber;
        }
    }
}
