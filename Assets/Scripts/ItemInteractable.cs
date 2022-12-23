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
            UIInstance = Instantiate(selectRoomUiPrefab);

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
            UIInstance.SetActive(false);
        }
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void chooseRoom()
    {
        // montre un hud de choix de salle
        pending = mod;
        UIInstance.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;
    }

    public static void applyEffectToRoom(int roomNumber)
    {
        //ajoute l'effet a la chambre donnee
        UIInstance.SetActive(false);
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
