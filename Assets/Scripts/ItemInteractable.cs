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

    public void applyEffectToRoom(int roomNumber)
    {
        if (!isWaiting)
            return;

        setPlayerInput(true);

        // Get the type of the mod attribute
        var modType = mod.GetType();

        // Check if the game object already has a component of the same type
        var existingComponent = Map.rooms[roomNumber].GetComponent(modType);
        if (existingComponent != null)
        {
            // Increment the count value of the existing component
            ((Modifier)existingComponent).count++;
        }
        else
        {
            // Add a new component of the same type as the mod attribute
            var newComponent = Map.rooms[roomNumber].AddComponent(modType);

            // Copy the values of the properties of the mod attribute to the new component
            var componentType = this.GetType();
            var properties = componentType.GetProperties();
            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite && property.Name != "name")
                {
                    var value = property.GetValue(this);

                    property.SetValue(newComponent, value);
                }
            }
            Map.rooms[roomNumber].GetComponent<Room>().modifiers.Add((Modifier)newComponent);
        }
        isWaiting = false;
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
