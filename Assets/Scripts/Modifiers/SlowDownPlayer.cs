using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPlayer : Downgrade
{
    private PlayerMovement pm;
    public static float percentageRemoved = .25f;

    private new void Start()
    {
        base.Start();
        pm = map.player.GetComponent<PlayerMovement>();
    }

    public override void effect()
    {
        //Debug.Log("Ciggie effect : " + id);
        if (id == 1)
        {
            pm.speed -= pm.defaultSpeed * percentageRemoved;
        }
        else
            stackEffect();
    }

    protected override void stackEffect()
    {
        pm.speed -= pm.defaultSpeed * (percentageRemoved * (1 / id));
    }
}
