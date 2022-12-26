using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// modifiers are applied per room so theoretically
// affecting the Map.currentRoom would work since they get called in onEnabled,
// a reset of every stat would need to be done between each room
public abstract class Modifier : MonoBehaviour
{
    public bool isPicked = false;
    public int count = 1;
    protected Map map;
    public string interactText = "Pick up";

    protected void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    }
    public abstract void effect(); // does the effect
    protected abstract void stackEffect(int index);
}
