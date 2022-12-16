using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMissileAttack : AIAction
{
    public override void Reset()
    {
        _brain.Rigid.velocity = Vector2.zero;

        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 1);
    }

    public override void TakeAction()
    {
        if(IsPlayAction) return;
        
        Vector3 originPos = _brain.transform.position;
        Vector3 targetDir = _brain.Player.position - originPos; 

        BossBullet bullet = PoolManager.Instance.Pop("BossBullet") as BossBullet;
        bullet.transform.position = originPos;
    }
}
