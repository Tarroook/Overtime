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

    public void chooseRoom()
    {
        CanvasGroup cg = UIInstance.GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;
    }

    public void applyEffectToRoom(int roomNumber)
    {
        if (!isWaiting)
            return;

        CanvasGroup cg = UIInstance.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerShooting>().enabled = true;

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
        chooseRoom();
        if (onPickedItem != null)
            onPickedItem(mod);

        mod.isPicked = true;

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
}
