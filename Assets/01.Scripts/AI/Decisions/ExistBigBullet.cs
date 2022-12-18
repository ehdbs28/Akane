using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistBigBullet : AIDecision
{
    private BigBullet _bullet;
    private bool NullCheak = false;

    public override bool MakeADecision()
    {   
       _bullet =  GameManager.Instance.GetComponentInChildren<BigBullet>();

        if(_bullet != null){
            return _bullet.IsBulletDisable || !_bullet.gameObject.activeSelf;
        }
        else
            return false;
    }
}
