using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPlayer : Downgrade
{
    public override void effect()
    {
        map.player.GetComponent<PlayerMovement>().speed -= map.player.GetComponent<PlayerMovement>().defaultSpeed * .25f;
    }
}
