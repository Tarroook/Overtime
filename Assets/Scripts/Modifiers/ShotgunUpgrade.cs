using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : Upgrade
{
    public override void effect()
    {
        PlayerShooting ps = map.player.GetComponent<PlayerShooting>();
        ps.nbPerShot++;
        ps.spread += 10;
    }
}
