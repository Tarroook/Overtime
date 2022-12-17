using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public Modifier mod;
    public void chooseRoom()
    {
        // montre un hud de choix de salle

    }

    public void applyEffectToRoom(int roomNumber)
    {
        //ajoute l'effet a la chambre donnee
        Map.currentRoom.modifiers.Add(mod);
    }
}
