using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteractable : Interactable
{
    public Modifier mod;
    public GameObject selectRoomUiPrefab;
    public static GameObject UIInstance;
    public static GameObject player;

    public static Modifier pending;

    private void Start()
    {
        mod = gameObject.GetComponent<Modifier>();
        if (UIInstance == null)
        {
            UIInstance = GameObject.FindGameObjectWithTag("ModUI");

            int count = 0;
            foreach (Transform child in UIInstance.transform)
            {
                Button b = child.GetComponent<Button>();
                if (b != null)
                {
                    int roomNumber = count;  // capture the value of count in a local variable
                    b.onClick.AddListener(() => applyEffectToRoom(roomNumber));  // pass the captured value to the lambda expression
                    count++;
                }
            }
        }
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void chooseRoom()
    {
        pending = mod;

        CanvasGroup cg = UIInstance.GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;
    }

    public static void applyEffectToRoom(int roomNumber)
    {
        CanvasGroup cg = UIInstance.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerShooting>().enabled = true;

        //Debug.Log("total = " + Map.rooms.Count + " added to room " + roomNumber);
        Map.rooms[roomNumber].GetComponent<Room>().modifiers.Add(pending);
        int count = 0;
        foreach (Modifier mod in Map.rooms[roomNumber].GetComponent<Room>().modifiers)
        {
            if (mod.GetType() == pending.GetType())
                count++;
        }
        pending.id = count;
        pending = null;
    }

    public override void interact()
    {
        chooseRoom();
        mod.isPicked = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
}
