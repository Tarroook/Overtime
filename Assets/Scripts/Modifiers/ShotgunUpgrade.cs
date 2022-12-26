using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : Upgrade
{
    private PlayerShooting ps;

    public override void effect()
    {
        loadParameters();
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                ps.bulletsPerShot++;
                ps.spread += 10;
            }
            else
                stackEffect(i);
        }
    }

    protected override void stackEffect(int index)
    {
        ps.bulletsPerShot++;
        ps.spread += 3;
    }

    protected override void loadParameters()
    {
        base.loadParameters();
        if (ps == null)
            ps = map.player.GetComponent<PlayerShooting>();
    }
}
