using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAttackAction : AIAction
{
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
        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 2);

        _brain.Rigid.velocity = _moveDir * _rollingSpeed;
        _lastVelocity = _brain.Rigid.velocity;

        Collider2D hitWall = Physics2D.OverlapCircle(_circleCollider.offset, _circleCollider.radius, _whatIsWallLayer);
        if(hitWall){
        }
    }
}
