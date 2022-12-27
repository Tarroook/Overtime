using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierInteractable : Interactable
{
    public Modifier mod;
    public static GameObject UIInstance;
    public static GameObject player;

    public delegate void pickedItemAction(Modifier modPicked);
    public static event pickedItemAction onPickedItem;

    private bool isWaiting = false;
    [Space(10)]
    public ParticleSystem holyParticle;
    public ParticleSystem cursedParticle;

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

        spawnParticles();
    }

    public override void interact()
    {
        isWaiting = true;

        setPlayerInput(false);

        if (onPickedItem != null)
            onPickedItem(mod);
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
            ((Modifier)existingComponent).count++;
        }
        else
        {
            // Add a new component of the same type as the mod attribute
            var newComponent = Map.rooms[roomNumber].AddComponent(modType);
        }
        isWaiting = false;
        Destroy(gameObject);
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

    private void spawnParticles()
    {
        Transform part = null;
        if (mod is Upgrade)
        {
            part = Instantiate(holyParticle, transform).transform;
        }
        else if (mod is Downgrade)
        {
            part = Instantiate(cursedParticle, transform).transform;
        }
        if (part != null)
            part.localPosition = Vector3.zero;
    }
}
