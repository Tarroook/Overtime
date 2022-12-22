using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : Upgrade
{
    private PlayerShooting ps;


    private new void Start()
    {
        base.Start();
        ps = map.player.GetComponent<PlayerShooting>();
    }

    public override void effect()
    {
        if (ps.bulletsPerShot == ps.defaultBulletsPerShot)
        {
            ps.bulletsPerShot++;
            ps.spread += 10;
        }
        else
            stackEffect();
    }

    protected override void stackEffect()
    {
        ps.bulletsPerShot++;
        ps.spread += 3;
    }
}
