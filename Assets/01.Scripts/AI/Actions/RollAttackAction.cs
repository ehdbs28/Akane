using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAttackAction : AIAction
{
    [ColorUsage(true, true)][SerializeField] private Color _bulletPhase2Color;

    [SerializeField] private float _rollingSpeed = 5f;
    [SerializeField] private LayerMask _whatIsWallLayer;

    private CircleCollider2D _circleCollider;
    private Vector3 _moveDir = new Vector3(1, 1, 0);
    private Vector3 _lastVelocity;

    private void Start() {
        _circleCollider  = _brain.GetComponent<CircleCollider2D>();
    }

    public override void TakeAction()
    {
        _lastVelocity = _brain.Rigid.velocity;
        _brain.Collider.isTrigger = true;

        RaycastHit2D hit = Physics2D.CircleCast(_brain.transform.position + (Vector3)_circleCollider.offset, _circleCollider.bounds.extents.x, _lastVelocity.normalized, 0.1f, _whatIsWallLayer);
        if(hit.collider){
            Vector3 replectVec = Vector3.Reflect(_lastVelocity, hit.normal);
            BulletCreatePattern();
            _brain.Rigid.velocity = replectVec.normalized * ((_brain.Boss.IsPhase) ? _rollingSpeed * 2 : _rollingSpeed);
        }
    }

    public override void Reset()
    {
        _brain.Rigid.velocity = Vector2.zero;
        _brain.Rigid.velocity = _moveDir * ((_brain.Boss.IsPhase) ? _rollingSpeed * 2 : _rollingSpeed);

        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 2);
    }

    private void BulletCreatePattern(){
        int startAngle = 0;
        int endAngle = 360;
        int angleInterval = 60;

        Vector3 originPos = _brain.transform.position;

        for(int angle = startAngle; angle < endAngle; angle += angleInterval){
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Rad2Deg));

            BossBullet bullet = PoolManager.Instance.Pop("BossBullet") as BossBullet;
            bullet.transform.position = originPos;
            bullet.SetVelocity(dir);
            if(_brain.Boss.IsPhase) bullet.SetBulletColor(_bulletPhase2Color);
        }
    }
}
