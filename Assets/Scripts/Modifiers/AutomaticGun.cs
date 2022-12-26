using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGun : Upgrade
{
    public override void effect()
    {
        loadParameters();
        map.player.GetComponent<PlayerShooting>().shootType = "auto";
    }

    protected override void stackEffect(int index)
    {
        throw new System.NotImplementedException();
    }
}
