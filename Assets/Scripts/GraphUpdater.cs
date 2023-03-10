using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class GraphUpdater : MonoBehaviour // this script updates the path, would be better if I had a way to check length and width of the tilemaps but eh
{
    public float pathDistanceMultiplier = 1f;
    public float nodeSize = 1f;

    private void OnEnable()
    {
        Map.onRoomLoaded += updateGraph;
    }
    private void OnDisable()
    {
        Map.onRoomLoaded -= updateGraph;
    }

    void updateGraph()
    {
        AstarPath.active.heuristic = Heuristic.Manhattan;
        AstarPath.active.heuristicScale = pathDistanceMultiplier;
        // Find the maximum width and height of the tilemaps in the current room
        int maxWidth = 0;
        int maxHeight = 0;
        
        foreach (Transform child in Map.currentRoom.transform)
        {
            // Iterate through the grandchildren of the currentRoom game object
            foreach (Transform grandchild in child)
            {
                Tilemap tilemap = grandchild.GetComponent<Tilemap>();
                if (tilemap != null)
                {
                    Vector3Int size = tilemap.size;
                    maxWidth = Mathf.Max(maxWidth, size.x);
                    maxHeight = Mathf.Max(maxHeight, size.y);
                }
            }
        }

        // Update the width and height of the grid used by the A* Pathfinding system
        AstarPath.active.data.gridGraph.SetDimensions(Mathf.RoundToInt(maxWidth * (1 / nodeSize)), Mathf.RoundToInt(maxHeight * (1 / nodeSize)), nodeSize);
        AstarPath.active.data.gridGraph.erodeIterations = Mathf.RoundToInt(1 / nodeSize) - 1;
        // Scan the scene and update the graphs
        AstarPath.active.Scan();
    }
}
