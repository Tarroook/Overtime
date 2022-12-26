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
        for(int i = 0; i < count; i++)
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
}
