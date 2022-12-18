using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// modifiers are applied per room so theoretically
// affecting the Map.currentRoom would work since they get called in onEnabled,
// a reset of every stat would need to be done between each room
public abstract class Modifier : MonoBehaviour
{
    [SerializeField]protected Map map;
    private void Start()
    {
        Debug.Log("Started Modifier");
        map = GameObject.Find("Map").GetComponent<Map>();
        Debug.Log(map);
    }
    public abstract void effect(); // does the effect
}
