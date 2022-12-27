using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// modifiers are applied per room so theoretically
// affecting the Map.currentRoom would work since they get called in onEnabled,
// a reset of every stat would need to be done between each room
public abstract class Modifier : MonoBehaviour
{
    public int count = 1;
    protected Map map;
    public string interactText = "Pick up";
    public abstract void effect(); // does the effect
    protected abstract void stackEffect(int index);

    protected virtual void loadParameters()
    {
        if (map == null)
            map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    }
}
