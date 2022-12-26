using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get10Hp : Upgrade
{
    private Health health;

    public override void effect()
    {
        loadParameters();
        health.heal(10);
    }

    protected override void stackEffect(int index)
    {
        throw new System.NotImplementedException();
    }

    protected override void loadParameters()
    {
        base.loadParameters();
        if (health == null)
            health = map.player.GetComponent<Health>();
    }
}
