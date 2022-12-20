using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttackAction : AIAction
{
    [ColorUsage(true, true)][SerializeField] private Color _bulletPhase2Color;

    private Vector3 _origin;

    private float _startAngle;
    private float _endAngle;
    private float _angleInterval = 20;

    public override void Reset()
    {
        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 3);
        IsPlayAction = false;
    }

    public override void TakeAction()
    {
        if(IsPlayAction) return;

        _brain.Rigid.velocity = Vector2.zero;
        _origin = _brain.transform.position;

        Vector3 _targetPos = _brain.Player.position - _origin;
        _startAngle = Mathf.Atan2(_targetPos.y, _targetPos.x) * Mathf.Rad2Deg - ((_brain.Boss.IsPhase) ? 0 : _angleInterval);
        _endAngle = Mathf.Atan2(_targetPos.y, _targetPos.x) * Mathf.Rad2Deg + ((_brain.Boss.IsPhase) ? 360 : _angleInterval);

        SoundManager.Instance.PlayOneShot(GameManager.Instance.BossSource, "BulletSpawn");

        for(float angle = _startAngle; angle <= _endAngle; angle += _angleInterval){
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            BossBullet bullet = PoolManager.Instance.Pop("BossBullet") as BossBullet;
            bullet.transform.position = _origin;
            if(_brain.Boss.IsPhase) bullet.BulletSpeed = 10; 
            bullet.SetVelocity(((_origin + spawnPos) - _origin).normalized);
            if(_brain.Boss.IsPhase) bullet.SetBulletColor(_bulletPhase2Color);
        }
        IsPlayAction = true;
    }
}
