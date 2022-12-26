using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteractable : Interactable
{
    public Modifier mod;
    public static GameObject UIInstance;
    public static GameObject player;

    public delegate void pickedItemAction(Modifier modPicked);
    public static event pickedItemAction onPickedItem;

    private bool isWaiting = false;

    private void OnEnable()
    {
        ChooseRoomButton.onSelectedRoomButton += applyEffectToRoom;
    }

    private void Start()
    {
        mod = gameObject.GetComponent<Modifier>();
        if (UIInstance == null)
            UIInstance = GameObject.FindGameObjectWithTag("ModUI");
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void applyEffectToRoom(int roomNumber)
    {
        if (!isWaiting)
            return;

        setPlayerInput(true);

        //Debug.Log("total = " + Map.rooms.Count + " added to room " + roomNumber);
        Map.rooms[roomNumber].GetComponent<Room>().modifiers.Add(mod);
        int count = 0;
        foreach (Modifier mod in Map.rooms[roomNumber].GetComponent<Room>().modifiers)
        {
            if (mod.GetType() == mod.GetType())
                count++;
        }
        mod.id = count;
        isWaiting = false;
    }

    public override void interact()
    {
        isWaiting = true;

        setPlayerInput(false);

        if (onPickedItem != null)
            onPickedItem(mod);

        mod.isPicked = true;

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    private void setPlayerInput(bool isEnabled) // gotta find a clean way to move this to player. Maybe make a Player class ?
    {
        player.GetComponent<PlayerMovement>().enabled = isEnabled;
        player.GetComponent<PlayerShooting>().enabled = isEnabled;
        foreach (Transform child in player.transform)
        {
            if (child.gameObject.GetComponent<Interact>() != null)
            {
                child.gameObject.GetComponent<Interact>().enabled = isEnabled;
                break;
            }
        }
    }
}
