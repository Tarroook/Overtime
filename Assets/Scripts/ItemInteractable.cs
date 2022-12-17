using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Modifier))]
public class ItemInteractable : Interactable
{
    public Modifier mod;
    private void Start()
    {
        mod = gameObject.GetComponent<Modifier>();
    }

    public void chooseRoom()
    {
        // montre un hud de choix de salle

    }

    public void applyEffectToRoom(int roomNumber)
    {
        //ajoute l'effet a la chambre donnee
        Map.rooms[roomNumber].GetComponent<Room>().modifiers.Add(mod);
    }

    public override void interact()
    {
        chooseRoom();
        Destroy(gameObject);
    }
}
