using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get10Hp : Upgrade
{
    public override void effect()
    {
        map.player.GetComponent<Health>().heal(10);
    }
}
