using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Downgrade : Modifier
{
    public ParticleSystem cursedParticle;

    protected new void Start()
    {
        base.Start();
        Transform part = Instantiate(cursedParticle, transform).transform;
        part.localPosition = Vector3.zero;
    }
}
