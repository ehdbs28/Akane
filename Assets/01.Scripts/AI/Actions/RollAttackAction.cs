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
        _lastVelocity = _brain.Rigid.velocity;

        RaycastHit2D hit = Physics2D.CircleCast(_brain.transform.position + (Vector3)_circleCollider.offset, _circleCollider.bounds.extents.x, _lastVelocity.normalized, 0.1f, _whatIsWallLayer);
        if(hit.collider){
            Vector3 replectVec = Vector3.Reflect(_lastVelocity, hit.normal);
            Debug.Log(replectVec.normalized);
            _brain.Rigid.velocity = replectVec.normalized * _rollingSpeed;
        }
    }

    public override void Reset()
    {
        _brain.Rigid.velocity = Vector2.zero;
        _brain.Rigid.velocity = _moveDir * _rollingSpeed;

        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 2);
    }
}
