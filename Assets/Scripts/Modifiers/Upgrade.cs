using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : Modifier
{
    public ParticleSystem holyParticle;

    protected new void Start()
    {
        base.Start();
        Transform part = Instantiate(holyParticle, transform).transform;
        part.localPosition = Vector3.zero;
    }
}
