using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttackAction : AIAction
{
    [SerializeField] private float _angle;

    private float _attackDistance = 1f;
    private Vector3 _origin;

    public override void TakeAction()
    {
        if(IsPlayAction is true) return;

        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 3);

        _brain.Rigid.velocity = Vector2.zero;
        _origin = _brain.transform.position;

        for(float angle = -_angle; angle <= _angle; angle += _angle){
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            BossBullet bullet = PoolManager.Instance.Pop("BossBullet") as BossBullet;
            bullet.transform.position = _origin;
            bullet.SetVelocity(((_origin + spawnPos) - _origin).normalized);
        }
        IsPlayAction = true;
    }
}
