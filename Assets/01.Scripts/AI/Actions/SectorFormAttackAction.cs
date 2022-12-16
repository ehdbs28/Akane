using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttackAction : AIAction
{
    private Vector3 _origin;

    private float _startAngle;
    private float _endAngle;
    private float _angleInterval = 20;

    public override void Reset()
    {
        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 3);
    }

    public override void TakeAction()
    {
        if(IsPlayAction) return;

        _brain.Rigid.velocity = Vector2.zero;
        _origin = _brain.transform.position;

        Vector3 _targetPos = _brain.Player.position - _origin;
        _startAngle = Mathf.Atan2(_targetPos.y, _targetPos.x) * Mathf.Rad2Deg - _angleInterval;
        _endAngle = Mathf.Atan2(_targetPos.y, _targetPos.x) * Mathf.Rad2Deg + _angleInterval;

        for(float angle = _startAngle; angle <= _endAngle; angle += _angleInterval){
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            BossBullet bullet = PoolManager.Instance.Pop("BossBullet") as BossBullet;
            bullet.transform.position = _origin;
            bullet.SetVelocity(((_origin + spawnPos) - _origin).normalized);
            bullet.SetBulletColor(Color.blue);
        }
        IsPlayAction = true;
    }
}
