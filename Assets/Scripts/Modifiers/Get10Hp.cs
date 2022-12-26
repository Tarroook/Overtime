using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get10Hp : Upgrade
{
    private Health health;

    private new void Start()
    {
        base.Start();
        health = map.player.GetComponent<Health>();
    }

    public override void effect()
    {
        health.heal(10);
    }

    protected override void stackEffect(int index)
    {
        throw new System.NotImplementedException();
    }
}
