using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Chaser : Enemy
{
    private new void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (!isAwake)
        {
            if (zone.awake)
            {
                isAwake = true;
            }
        }
        if(currentPath != null)
        {
            if (currentPath.Count == 0 || Mathf.Abs(currentPath[currentPath.Count - 1].x - player.transform.position.x) > distanceTillRecalculate || Mathf.Abs(currentPath[currentPath.Count - 1].y - player.transform.position.y) > distanceTillRecalculate)
            {
                calculatePathTowards(player.transform.position);
            }
        }
        else
        {
            calculatePathTowards(player.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if(isAwake)
            walkAndRotateAlongPath();
    }
}
