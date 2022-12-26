using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPlayer : Downgrade
{
    private PlayerMovement pm;
    public static float percentageRemoved = .25f;

    public override void effect()
    {
        loadParameters();
        //Debug.Log("Ciggie effect : " + id);
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                pm.speed -= pm.defaultSpeed * percentageRemoved;
                Debug.Log("Ciggie effect : " + i);
            }
            else
                stackEffect(i + 1);
        }
    }

    protected override void stackEffect(int index)
    {
        //Debug.Log("speed = " + pm.speed + " - " + pm.defaultSpeed * (percentageRemoved * (1f / id)));
        pm.speed -= pm.defaultSpeed * (percentageRemoved * (1f / index));
    }

    protected override void loadParameters()
    {
        base.loadParameters();
        if(pm == null)
            pm = map.player.GetComponent<PlayerMovement>();
    }
}
