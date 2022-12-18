using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAction : AIAction
{
    public override void Reset()
    {

    }

    public override void TakeAction()
    {
        if(IsPlayAction) return;

        PoolingParticle stunParticle = PoolManager.Instance.Pop("StunParticle") as PoolingParticle;
        stunParticle.SetPosition(new Vector3(_brain.Collider.bounds.center.x, _brain.Collider.bounds.max.y));
        stunParticle.Play();

        IsPlayAction = true;   
    }
}
