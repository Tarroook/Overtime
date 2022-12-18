using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGun : Upgrade
{
    public override void effect()
    {
        map.player.GetComponent<PlayerShooting>().shootType = "auto";
    }
}
