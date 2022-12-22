using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPlayer : Downgrade
{
    public override void effect()
    {
        if (map.player.GetComponent<PlayerMovement>().speed == map.player.GetComponent<PlayerMovement>().defaultSpeed)
        {
            map.player.GetComponent<PlayerMovement>().speed -= map.player.GetComponent<PlayerMovement>().defaultSpeed * .25f;
        }
        else
            stackEffect();
    }

    protected override void stackEffect()
    {
        map.player.GetComponent<PlayerMovement>().speed -= map.player.GetComponent<PlayerMovement>().speed * .25f;
    }
}
